using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace RogueDungeon.Services.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SafeEnumerable<T>(this IEnumerable<T> source) => 
            source ?? Enumerable.Empty<T>();

        public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> source) => 
            source == null || !source.Any();

        public static IEnumerable<T> NonSharedItems<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstSet = new HashSet<T>(first);
            var secondSet = new HashSet<T>(second);
            return firstSet.Where(x => !secondSet.Contains(x)).Concat(secondSet.Where(x => !firstSet.Contains(x)));
        }

        public static List<T> ToListOrNull<T>(this IEnumerable<T> source) => 
            !(source?.Any() ?? false) ? null : source.ToList();

        public static IEnumerable<T> GetAll<T>(this IEnumerable<object> source, Predicate<T> predicate = null) => 
            source.Where(n => n is T item && (predicate?.Invoke(item) ?? false)).Cast<T>();

        public static T Get<T>(this IEnumerable<object> source, Predicate<T> predicate = null) =>
            source.GetAll(predicate).First();
        
        public static T GetOrDefault<T>(this IEnumerable<object> source, Predicate<T> predicate = null) =>
            source.TryGet(out var result, predicate) ? result : default;
        
        public static bool TryGet<T>(this IEnumerable<object> source, out T result, Predicate<T> predicate = null) =>
            (result = source.GetAll(predicate).FirstOrDefault()) != null;
    }
}