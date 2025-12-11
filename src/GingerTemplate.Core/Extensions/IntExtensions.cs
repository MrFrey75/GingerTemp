namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for integers.
/// </summary>
public static class IntExtensions
{
    /// <summary>
    /// Checks if an integer is even.
    /// </summary>
    public static bool IsEven(this int number)
    {
        return number % 2 == 0;
    }

    /// <summary>
    /// Checks if an integer is odd.
    /// </summary>
    public static bool IsOdd(this int number)
    {
        return number % 2 != 0;
    }
}
