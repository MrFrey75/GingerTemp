namespace GingerTemplate.Core.Enums;

/// <summary>
/// Enumeration for user roles in the system.
/// </summary>
public enum UserRole
{
    Admin,
    Manager,
    User,
    Guest
}

/// <summary>
/// Enumeration for user account status.
/// </summary>
public enum UserStatus
{
    Active,
    Inactive,
    Locked,
    Suspended
}
