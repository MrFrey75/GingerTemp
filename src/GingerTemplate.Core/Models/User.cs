namespace GingerTemplate.Core.Models;

/// <summary>
/// Represents a user in the system.
/// </summary>
public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public int LoginAttempts { get; set; }

    public bool IsLocked { get; set; }

    public string? FullName => $"{FirstName} {LastName}".Trim();
}
