namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for TimeSpan.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts a TimeSpan to a human-readable string.
    /// </summary>
    public static string ToHumanReadableString(this TimeSpan timeSpan)
    {
        return string.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
            timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
}
