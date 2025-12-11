namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for decimal values.
/// </summary>
public static class DecimalExtensions
{
    /// <summary>
    /// Converts a decimal to a percentage string.
    /// </summary>
    public static string ToPercentageString(this decimal value, int decimalPlaces = 2)
    {
        return (value * 100).ToString($"F{decimalPlaces}") + "%";
    }
}
