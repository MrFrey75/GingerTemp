using System;
using System.Collections.Generic;
using GingerTemplate.Core.Enums;

namespace GingerTemplate.Core.Models;

/// <summary>
/// Represents a notification message
/// </summary>
public class Notification
{
    public Guid Id { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationChannel Channel { get; set; }
    public NotificationPriority Priority { get; set; }
    public NotificationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public Dictionary<string, string> Metadata { get; set; } = new();
    public string? ErrorMessage { get; set; }
    public int RetryCount { get; set; }

    public Notification()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Status = NotificationStatus.Pending;
        Priority = NotificationPriority.Normal;
    }
}
