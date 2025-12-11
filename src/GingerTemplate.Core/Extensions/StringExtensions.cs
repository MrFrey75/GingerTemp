namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for string objects.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Checks if a string is null or empty.
    /// </summary>
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Checks if a string is null, empty, or whitespace.
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// Truncates a string to a maximum length.
    /// </summary>
    public static string Truncate(this string value, int maxLength)
    {
        if (value.IsNullOrEmpty())
            return value;

        return value.Length <= maxLength ? value : value[..maxLength];
    }

    /// <summary>
    /// Converts a string to title case.
    /// </summary>
    public static string ToTitleCase(this string value)
    {
        if (value.IsNullOrEmpty())
            return value;

        var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
        return textInfo.ToTitleCase(value.ToLower());
    }
}
