using System;

namespace GingerTemplate.Core.Utilities;

/// <summary>
/// Lightweight guard clauses for argument validation.
/// </summary>
public static class Guard
{
    public static T NotNull<T>(T value, string paramName) where T : class
    {
        if (value is null) throw new ArgumentNullException(paramName);
        return value;
    }

    public static string NotNullOrWhiteSpace(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", paramName);
        return value;
    }

    public static T NotDefault<T>(T value, string paramName) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default)) throw new ArgumentException("Value cannot be the default value.", paramName);
        return value;
    }

    public static int GreaterThanZero(int value, string paramName)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(paramName, "Value must be greater than zero.");
        return value;
    }

    public static double GreaterThanZero(double value, string paramName)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(paramName, "Value must be greater than zero.");
        return value;
    }

    public static T InRange<T>(T value, T min, T max, string paramName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(paramName, $"Value must be between {min} and {max}.");
        return value;
    }
}
