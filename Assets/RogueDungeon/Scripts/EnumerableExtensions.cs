using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;

namespace RogueDungeon
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SafeEnumerable<T>(this IEnumerable<T> source) => 
            source ?? Enumerable.Empty<T>();

        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> source) => 
            source is null || !source.Any();

        public static IEnumerable<T> NonSharedItems<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstSet = new HashSet<T>(first);
            var secondSet = new HashSet<T>(second);
            return firstSet.Where(x => !secondSet.Contains(x)).Concat(secondSet.Where(x => !firstSet.Contains(x)));
        }

        public static List<T> ToListOrNull<T>(this IEnumerable<T> source) => 
            !source.Any() ? null : source.ToList();
    }
}