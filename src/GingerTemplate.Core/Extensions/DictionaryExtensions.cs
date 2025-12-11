using System.Collections.Generic;

namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for dictionaries.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Merges two dictionaries. In case of key conflicts, values from the second dictionary overwrite those from the first.
    /// </summary>
    public static Dictionary<TKey, TValue> MergeDictionaries<TKey, TValue>(this Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, TValue>(first);
        foreach (var kvp in second)
        {
            result[kvp.Key] = kvp.Value;
        }
        return result;
    }
}
