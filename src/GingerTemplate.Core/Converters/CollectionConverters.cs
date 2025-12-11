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
}
