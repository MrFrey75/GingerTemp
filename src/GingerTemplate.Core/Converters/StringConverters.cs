using System;
using System.Globalization;

namespace GingerTemplate.Core.Converters;

/// <summary>
/// Safe string conversion helpers for common primitive types.
/// </summary>
public static class StringConverters
{
    public static bool TryToInt(this string? value, out int result)
    {
        return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    }

    public static int ToIntOrDefault(this string? value, int defaultValue = 0)
    {
        return value.TryToInt(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToLong(this string? value, out long result)
    {
        return long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
    }

    public static long ToLongOrDefault(this string? value, long defaultValue = 0)
    {
        return value.TryToLong(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToDecimal(this string? value, out decimal result)
    {
        return decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
    }

    public static decimal ToDecimalOrDefault(this string? value, decimal defaultValue = 0m)
    {
        return value.TryToDecimal(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToDouble(this string? value, out double result)
    {
        return double.TryParse(value, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out result);
    }

    public static double ToDoubleOrDefault(this string? value, double defaultValue = 0d)
    {
        return value.TryToDouble(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToBool(this string? value, out bool result)
    {
        return bool.TryParse(value, out result);
    }

    public static bool ToBoolOrDefault(this string? value, bool defaultValue = false)
    {
        return value.TryToBool(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToGuid(this string? value, out Guid result)
    {
        return Guid.TryParse(value, out result);
    }

    public static Guid ToGuidOrDefault(this string? value, Guid defaultValue)
    {
        return value.TryToGuid(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToDateTime(this string? value, out DateTime result)
    {
        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out result);
    }

    public static DateTime ToDateTimeOrDefault(this string? value, DateTime defaultValue)
    {
        return value.TryToDateTime(out var parsed) ? parsed : defaultValue;
    }

    public static bool TryToDateTimeOffset(this string? value, out DateTimeOffset result)
    {
        return DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out result);
    }

    public static DateTimeOffset ToDateTimeOffsetOrDefault(this string? value, DateTimeOffset defaultValue)
    {
        return value.TryToDateTimeOffset(out var parsed) ? parsed : defaultValue;
    }
}
