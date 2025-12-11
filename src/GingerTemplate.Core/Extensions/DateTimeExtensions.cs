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

    /// <summary>
    /// Converts a DateTime to Unix epoch seconds.
    /// </summary>
    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds();  
    }

    /// <summary>
    /// Creates a DateTime from Unix epoch seconds.
    /// </summary>
    public static DateTime FromUnixTimeSeconds(this long seconds)
    {
        return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
    }
}
