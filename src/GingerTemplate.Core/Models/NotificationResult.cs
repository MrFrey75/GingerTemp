using System;

namespace GingerTemplate.Core.Models;

/// <summary>
/// Result of a notification send operation
/// </summary>
public class NotificationResult
{
    public bool Success { get; set; }
    public Guid NotificationId { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; }

    public NotificationResult()
    {
        Timestamp = DateTime.UtcNow;
    }

    public static NotificationResult SuccessResult(Guid notificationId)
    {
        return new NotificationResult
        {
            Success = true,
            NotificationId = notificationId
        };
    }

    public static NotificationResult FailureResult(string errorMessage)
    {
        return new NotificationResult
        {
            Success = false,
            ErrorMessage = errorMessage
        };
    }
}
