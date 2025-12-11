using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GingerTemplate.Core.Enums;
using GingerTemplate.Core.Exceptions;
using GingerTemplate.Core.Models;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Interface for notification service operations.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;INotificationService, NotificationService&gt;();
/// var notify = provider.GetRequiredService&lt;INotificationService&gt;();
/// await notify.SendNotificationAsync(new NotificationRequest
/// {
///     Recipient = "user@example.com",
///     Subject = "Welcome",
///     Message = "Hello!",
///     Channel = NotificationChannel.Email
/// });
/// </code>
/// </remarks>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification through the specified channel
    /// </summary>
    /// <param name="request">Notification request details</param>
    /// <returns>Result of the notification send operation</returns>
    Task<NotificationResult> SendNotificationAsync(NotificationRequest request);

    /// <summary>
    /// Sends notifications to multiple recipients
    /// </summary>
    /// <param name="requests">List of notification requests</param>
    /// <returns>List of notification results</returns>
    Task<IEnumerable<NotificationResult>> SendBulkNotificationsAsync(IEnumerable<NotificationRequest> requests);

    /// <summary>
    /// Gets the status of a notification by ID
    /// </summary>
    /// <param name="notificationId">The notification ID</param>
    /// <returns>The notification object</returns>
    Task<Notification?> GetNotificationStatusAsync(Guid notificationId);

    /// <summary>
    /// Gets notification history for a recipient
    /// </summary>
    /// <param name="recipient">Recipient identifier (email, phone, etc.)</param>
    /// <param name="limit">Maximum number of notifications to return</param>
    /// <returns>List of notifications</returns>
    Task<IEnumerable<Notification>> GetNotificationHistoryAsync(string recipient, int limit = 50);

    /// <summary>
    /// Retries a failed notification
    /// </summary>
    /// <param name="notificationId">The notification ID to retry</param>
    /// <returns>Result of the retry operation</returns>
    Task<NotificationResult> RetryNotificationAsync(Guid notificationId);

    /// <summary>
    /// Cancels a pending notification
    /// </summary>
    /// <param name="notificationId">The notification ID to cancel</param>
    /// <returns>True if cancelled successfully</returns>
    Task<bool> CancelNotificationAsync(Guid notificationId);
}

/// <summary>
/// Implementation of notification service supporting multiple channels
/// </summary>
public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly IEmailService _emailService;
    private readonly Dictionary<Guid, Notification> _notificationStore;
    private readonly Dictionary<string, List<Notification>> _recipientHistory;
    private readonly int _maxRetries = 3;

    public NotificationService(ILogger<NotificationService> logger, IEmailService emailService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("NotificationService initialized.");
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _notificationStore = new Dictionary<Guid, Notification>();
        _recipientHistory = new Dictionary<string, List<Notification>>();
    }

    /// <inheritdoc/>
    public async Task<NotificationResult> SendNotificationAsync(NotificationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (string.IsNullOrWhiteSpace(request.Recipient))
        {
            throw new ValidationException("Recipient cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ValidationException("Message cannot be empty");
        }

        var notification = new Notification
        {
            Recipient = request.Recipient,
            Subject = request.Subject,
            Message = request.Message,
            Channel = request.Channel,
            Priority = request.Priority,
            Metadata = request.Metadata ?? new Dictionary<string, string>()
        };

        _logger.LogInformation("Sending {Channel} notification to {Recipient} with priority {Priority}", 
            request.Channel, request.Recipient, request.Priority);

        try
        {
            await SendByChannelAsync(notification);
            
            notification.Status = NotificationStatus.Sent;
            notification.SentAt = DateTime.UtcNow;
            
            StoreNotification(notification);

            _logger.LogInformation("Notification {NotificationId} sent successfully via {Channel}", 
                notification.Id, notification.Channel);

            return NotificationResult.SuccessResult(notification.Id);
        }
        catch (Exception ex)
        {
            notification.Status = NotificationStatus.Failed;
            notification.ErrorMessage = ex.Message;
            
            StoreNotification(notification);

            _logger.LogError(ex, "Failed to send notification {NotificationId} via {Channel} to {Recipient}", 
                notification.Id, notification.Channel, notification.Recipient);

            return NotificationResult.FailureResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<NotificationResult>> SendBulkNotificationsAsync(IEnumerable<NotificationRequest> requests)
    {
        if (requests == null)
        {
            throw new ArgumentNullException(nameof(requests));
        }

        var requestList = requests.ToList();
        _logger.LogInformation("Sending bulk notifications: {Count} requests", requestList.Count);

        var results = new List<NotificationResult>();

        foreach (var request in requestList)
        {
            try
            {
                var result = await SendNotificationAsync(request);
                results.Add(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing bulk notification request");
                results.Add(NotificationResult.FailureResult(ex.Message));
            }
        }

        var successCount = results.Count(r => r.Success);
        _logger.LogInformation("Bulk notification completed: {Success}/{Total} successful", 
            successCount, requestList.Count);

        return results;
    }

    /// <inheritdoc/>
    public Task<Notification?> GetNotificationStatusAsync(Guid notificationId)
    {
        _logger.LogInformation("Retrieving notification status for {NotificationId}", notificationId);

        if (_notificationStore.TryGetValue(notificationId, out var notification))
        {
            return Task.FromResult<Notification?>(notification);
        }

        _logger.LogWarning("Notification {NotificationId} not found", notificationId);
        return Task.FromResult<Notification?>(null);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<Notification>> GetNotificationHistoryAsync(string recipient, int limit = 50)
    {
        if (string.IsNullOrWhiteSpace(recipient))
        {
            throw new ArgumentException("Recipient cannot be empty", nameof(recipient));
        }

        _logger.LogInformation("Retrieving notification history for {Recipient}, limit: {Limit}", 
            recipient, limit);

        if (_recipientHistory.TryGetValue(recipient, out var history))
        {
            var results = history
                .OrderByDescending(n => n.CreatedAt)
                .Take(limit)
                .ToList();

            _logger.LogInformation("Found {Count} notifications for {Recipient}", results.Count, recipient);
            return Task.FromResult<IEnumerable<Notification>>(results);
        }

        _logger.LogInformation("No notification history found for {Recipient}", recipient);
        return Task.FromResult<IEnumerable<Notification>>(new List<Notification>());
    }

    /// <inheritdoc/>
    public async Task<NotificationResult> RetryNotificationAsync(Guid notificationId)
    {
        _logger.LogInformation("Retrying notification {NotificationId}", notificationId);

        var notification = await GetNotificationStatusAsync(notificationId);

        if (notification == null)
        {
            _logger.LogWarning("Cannot retry: Notification {NotificationId} not found", notificationId);
            return NotificationResult.FailureResult("Notification not found");
        }

        if (notification.Status != NotificationStatus.Failed)
        {
            _logger.LogWarning("Cannot retry: Notification {NotificationId} status is {Status}", 
                notificationId, notification.Status);
            return NotificationResult.FailureResult($"Cannot retry notification with status: {notification.Status}");
        }

        if (notification.RetryCount >= _maxRetries)
        {
            _logger.LogWarning("Cannot retry: Notification {NotificationId} has exceeded max retries ({MaxRetries})", 
                notificationId, _maxRetries);
            return NotificationResult.FailureResult($"Maximum retry attempts ({_maxRetries}) exceeded");
        }

        notification.RetryCount++;
        notification.Status = NotificationStatus.Pending;
        notification.ErrorMessage = null;

        try
        {
            await SendByChannelAsync(notification);
            
            notification.Status = NotificationStatus.Sent;
            notification.SentAt = DateTime.UtcNow;

            _logger.LogInformation("Notification {NotificationId} retried successfully (attempt {RetryCount})", 
                notificationId, notification.RetryCount);

            return NotificationResult.SuccessResult(notification.Id);
        }
        catch (Exception ex)
        {
            notification.Status = NotificationStatus.Failed;
            notification.ErrorMessage = ex.Message;

            _logger.LogError(ex, "Failed to retry notification {NotificationId} (attempt {RetryCount})", 
                notificationId, notification.RetryCount);

            return NotificationResult.FailureResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public Task<bool> CancelNotificationAsync(Guid notificationId)
    {
        _logger.LogInformation("Cancelling notification {NotificationId}", notificationId);

        if (_notificationStore.TryGetValue(notificationId, out var notification))
        {
            if (notification.Status == NotificationStatus.Pending)
            {
                notification.Status = NotificationStatus.Cancelled;
                _logger.LogInformation("Notification {NotificationId} cancelled successfully", notificationId);
                return Task.FromResult(true);
            }

            _logger.LogWarning("Cannot cancel notification {NotificationId} with status {Status}", 
                notificationId, notification.Status);
            return Task.FromResult(false);
        }

        _logger.LogWarning("Notification {NotificationId} not found for cancellation", notificationId);
        return Task.FromResult(false);
    }

    /// <summary>
    /// Sends notification through the appropriate channel
    /// </summary>
    private async Task SendByChannelAsync(Notification notification)
    {
        switch (notification.Channel)
        {
            case NotificationChannel.Email:
                await SendEmailNotificationAsync(notification);
                break;

            case NotificationChannel.Sms:
                await SendSmsNotificationAsync(notification);
                break;

            case NotificationChannel.Push:
                await SendPushNotificationAsync(notification);
                break;

            case NotificationChannel.InApp:
                await SendInAppNotificationAsync(notification);
                break;

            default:
                throw new NotSupportedException($"Notification channel {notification.Channel} is not supported");
        }
    }

    /// <summary>
    /// Sends email notification
    /// </summary>
    private async Task SendEmailNotificationAsync(Notification notification)
    {
        _logger.LogDebug("Sending email notification to {Recipient}", notification.Recipient);

        try
        {
            await _emailService.SendEmailAsync(
                notification.Recipient,
                notification.Subject,
                notification.Message,
                isHtml: notification.Metadata.ContainsKey("IsHtml") && 
                       bool.Parse(notification.Metadata["IsHtml"])
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email notification");
            throw new ExternalServiceException("Email service error", ex);
        }

        notification.DeliveredAt = DateTime.UtcNow;
        notification.Status = NotificationStatus.Delivered;
    }

    /// <summary>
    /// Sends SMS notification
    /// </summary>
    private Task SendSmsNotificationAsync(Notification notification)
    {
        _logger.LogDebug("Sending SMS notification to {Recipient}", notification.Recipient);

        // SMS integration would go here (Twilio, AWS SNS, etc.)
        // For now, simulate successful send
        _logger.LogInformation("SMS sent to {PhoneNumber}: {Message}", 
            notification.Recipient, 
            notification.Message.Substring(0, Math.Min(50, notification.Message.Length)));

        notification.DeliveredAt = DateTime.UtcNow;
        notification.Status = NotificationStatus.Delivered;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends push notification
    /// </summary>
    private Task SendPushNotificationAsync(Notification notification)
    {
        _logger.LogDebug("Sending push notification to {Recipient}", notification.Recipient);

        // Push notification integration would go here (Firebase, OneSignal, etc.)
        // For now, simulate successful send
        _logger.LogInformation("Push notification sent to device {DeviceId}: {Message}", 
            notification.Recipient, 
            notification.Subject);

        notification.DeliveredAt = DateTime.UtcNow;
        notification.Status = NotificationStatus.Delivered;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends in-app notification
    /// </summary>
    private Task SendInAppNotificationAsync(Notification notification)
    {
        _logger.LogDebug("Sending in-app notification to {Recipient}", notification.Recipient);

        // In-app notification storage would go here (database, cache, etc.)
        // For now, simulate successful send
        _logger.LogInformation("In-app notification stored for user {UserId}: {Subject}", 
            notification.Recipient, 
            notification.Subject);

        notification.DeliveredAt = DateTime.UtcNow;
        notification.Status = NotificationStatus.Delivered;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Stores notification in memory store and recipient history
    /// </summary>
    private void StoreNotification(Notification notification)
    {
        _notificationStore[notification.Id] = notification;

        if (!_recipientHistory.ContainsKey(notification.Recipient))
        {
            _recipientHistory[notification.Recipient] = new List<Notification>();
        }

        _recipientHistory[notification.Recipient].Add(notification);
    }
}