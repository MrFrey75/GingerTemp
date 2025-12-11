namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Converts an enum value to its string representation.
    /// </summary>
    public static string ToEnumString<T>(this T enumValue) where T : Enum
    {
        return enumValue.ToString();
    }
}
