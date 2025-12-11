using System.Text.Json;

namespace GingerTemplate.Core.Extensions;

/// <summary>
/// JSON serialization helpers.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Serializes an object to a JSON string.
    /// </summary>
    public static string ToJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    /// <summary>
    /// Deserializes a JSON string to an object of type T.
    /// </summary>
    public static T FromJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json)!;
    }
}
