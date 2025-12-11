namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for DateTime.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a DateTime to a formatted string.
    /// </summary>
    public static string ToFormattedString(this DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
    {
        return dateTime.ToString(format);
    }
}
