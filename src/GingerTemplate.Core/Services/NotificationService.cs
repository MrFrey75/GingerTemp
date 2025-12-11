using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

public interface INotificationService
{
    void SendNotification(string message, string recipient);
}

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public void SendNotification(string message, string recipient)
    {
        // Simulate sending a notification
        _logger.LogInformation("Sending notification to {Recipient}: {Message}", recipient, message);
        // Actual notification sending logic would go here
    }
}