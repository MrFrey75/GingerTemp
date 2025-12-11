namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for nullable value types.
/// </summary>
public static class NullableExtensions
{
    /// <summary>
    /// Returns the value of a nullable type or a default value if null.
    /// </summary>
    public static T GetValueOrDefault<T>(this T? nullable, T defaultValue) where T : struct
    {
        return nullable ?? defaultValue;
    }
}
