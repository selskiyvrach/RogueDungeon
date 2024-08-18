using System.Collections.Generic;
using System.Linq;

namespace RogueDungeon
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SafeEnumerable<T>(this IEnumerable<T> source) => 
            source ?? Enumerable.Empty<T>();

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => 
            source is null || !source.Any();
    }
}