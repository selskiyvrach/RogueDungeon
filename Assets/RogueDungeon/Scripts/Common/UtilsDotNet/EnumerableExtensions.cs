using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Common.UtilsDotNet
{
    public static class EnumerableExtensions
    {
        public static string Join(this IEnumerable<string> source, string separator = ", ") => 
            string.Join(separator, source);
        
        public static string Join<T>(this IEnumerable<T> source, Func<T, string> selector, string separator = ", ") => 
            string.Join(separator, source.Select(selector));
        
        public static string JoinTypeNames<T>(this IEnumerable<T> source, string separator = ", ") => 
            string.Join(separator, source.Select(n => n.TypeName()));


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
            source.Where(n => n is T item && (predicate == null || predicate.Invoke(item))).Cast<T>();

        public static T Get<T>(this IEnumerable<object> source, Predicate<T> predicate = null) =>
            source.GetAll(predicate).FirstOrDefault();
        
        public static T Get<T>(this IEnumerable<T> source, Predicate<T> predicate) =>
            source.FirstOrDefault(predicate.Invoke);
        
        public static T GetOrDefault<T>(this IEnumerable<object> source, Predicate<T> predicate = null) =>
            source.TryGet(out var result, predicate) ? result : default;
        
        public static bool TryGet<T>(this IEnumerable<object> source, out T result, Predicate<T> predicate = null) =>
            (result = source.GetAll(predicate).FirstOrDefault()) != null;

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> source, Action<T> action) =>
            source.Select(n =>
            {
                action?.Invoke(n);
                return n;
            });

        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}