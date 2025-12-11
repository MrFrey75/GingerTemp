namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for double values.
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    /// Rounds a double to the specified number of decimal places.
    /// </summary>
    public static double RoundTo(this double value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces);
    }
}
