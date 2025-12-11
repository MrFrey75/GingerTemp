namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for boolean values.
/// </summary>
public static class BoolExtensions
{
    /// <summary>
    /// Converts a boolean to "Yes" or "No".
    /// </summary>
    public static string ToYesNo(this bool value)
    {
        return value ? "Yes" : "No";
    }

    /// <summary>
    /// Converts a boolean to "True" or "False".
    /// </summary>
    public static string ToTrueFalse(this bool value)
    {
        return value ? "True" : "False";
    }
}
