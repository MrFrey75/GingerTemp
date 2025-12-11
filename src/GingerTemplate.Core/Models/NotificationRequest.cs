using System.Collections.Generic;
using GingerTemplate.Core.Enums;

namespace GingerTemplate.Core.Models;

/// <summary>
/// Request to send a notification
/// </summary>
public class NotificationRequest
{
    public string Recipient { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationChannel Channel { get; set; }
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;
    public Dictionary<string, string>? Metadata { get; set; }
}
