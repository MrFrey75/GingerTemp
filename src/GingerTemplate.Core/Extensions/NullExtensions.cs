namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Base extension methods for null checks.
/// </summary>
public static class NullExtensions
{
    /// <summary>
    /// Checks if an object is null.
    /// </summary>
    public static bool IsNull(this object? obj)
    {
        return obj is null;
    }

    /// <summary>
    /// Checks if an object is not null.
    /// </summary>
    public static bool IsNotNull(this object? obj)
    {
        return obj is not null;
    }
}
