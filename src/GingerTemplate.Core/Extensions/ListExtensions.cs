using System.Collections.Generic;

namespace GingerTemplate.Core.Extensions;

/// <summary>
/// Extension methods for lists.
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Shuffles a list randomly.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        var rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }
    }
}
