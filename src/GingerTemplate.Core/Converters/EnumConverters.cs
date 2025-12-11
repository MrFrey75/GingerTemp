using System;

namespace GingerTemplate.Core.Converters;

/// <summary>
/// Generic enum conversion helpers.
/// </summary>
public static class EnumConverters
{
    public static bool TryToEnum<TEnum>(this string? value, out TEnum result) where TEnum : struct, Enum
    {
        return Enum.TryParse(value, ignoreCase: true, out result);
    }

    public static TEnum ToEnumOrDefault<TEnum>(this string? value, TEnum defaultValue) where TEnum : struct, Enum
    {
        return value.TryToEnum(out TEnum parsed) ? parsed : defaultValue;
    }
}
