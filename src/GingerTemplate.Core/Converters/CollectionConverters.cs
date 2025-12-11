using System;
using System.Collections.Generic;

namespace GingerTemplate.Core.Converters;

/// <summary>
/// Collection conversion helpers.
/// </summary>
public static class CollectionConverters
{
    /// <summary>
    /// Converts a sequence to a hash set using the provided comparer.
    /// </summary>
    public static HashSet<T> ToHashSetSafe<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer = null)
    {
        return source == null ? new HashSet<T>(comparer) : new HashSet<T>(source, comparer);
    }

    /// <summary>
    /// Converts an enumerable to a list. Returns an empty list when source is null.
    /// </summary>
    public static List<T> ToListSafe<T>(this IEnumerable<T> source)
    {
        return source == null ? new List<T>() : new List<T>(source);
    }

    /// <summary>
    /// Converts an enumerable to an array. Returns an empty array when source is null.
    /// </summary>
    /// <returns></returns>
    public static T[] ToArraySafe<T>(this IEnumerable<T> source)
    {
        return source == null ? Array.Empty<T>() : System.Linq.Enumerable.ToArray(source);
    }

    /// <summary>
    /// Converts a sequence to a dictionary using the provided key selector and comparer.
    /// </summary>
    public static Dictionary<TKey, TElement> ToDictionarySafe<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector,
        Func<TSource, TElement> elementSelector,
        IEqualityComparer<TKey>? comparer = null)
    {
        var dictionary = new Dictionary<TKey, TElement>(comparer);
        if (source == null)
        {
            return dictionary;  
        } else 
        {
            foreach (var item in source)
            {
                dictionary[keySelector(item)] = elementSelector(item);
            }
            return dictionary;
        }
    }

}
