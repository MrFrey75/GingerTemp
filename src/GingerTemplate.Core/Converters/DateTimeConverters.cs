using System;

namespace GingerTemplate.Core.Converters;

/// <summary>
/// Date and time conversion helpers.
/// </summary>
public static class DateTimeConverters
{
    private static readonly DateTimeOffset UnixEpoch = DateTimeOffset.UnixEpoch;

    /// <summary>
    /// Converts a DateTime to Unix epoch seconds (UTC assumed).
    /// </summary>
    public static long ToUnixTimeSeconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime.ToUniversalTime()).ToUnixTimeSeconds();
    }

    /// <summary>
    /// Converts a DateTimeOffset to Unix epoch seconds.
    /// </summary>
    public static long ToUnixTimeSeconds(this DateTimeOffset dateTimeOffset)
    {
        return dateTimeOffset.ToUniversalTime().ToUnixTimeSeconds();
    }

    /// <summary>
    /// Creates a DateTimeOffset from Unix epoch seconds.
    /// </summary>
    public static DateTimeOffset FromUnixTimeSeconds(this long seconds)
    {
        return UnixEpoch.AddSeconds(seconds);
    }

    /// <summary>
    /// Creates a DateTime from Unix epoch seconds (UTC).
    /// </summary>
    public static DateTime FromUnixTimeSecondsUtc(this long seconds)
    {
        return UnixEpoch.AddSeconds(seconds).UtcDateTime;
    }

    /// <summary>
    /// Creates a DateTime from Unix epoch seconds (local time).
    /// </summary>
    public static DateTime FromUnixTimeSecondsLocal(this long seconds)
    {
        return UnixEpoch.AddSeconds(seconds).LocalDateTime;
    }
}
