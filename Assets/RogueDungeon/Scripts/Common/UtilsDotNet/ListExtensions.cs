using System;
using System.Collections.Generic;

namespace Common.UtilsDotNet
{
    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> source) => 
            source[UnityEngine.Random.Range(0, source.Count)];

        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items, int count)
        {
            foreach (var item in items)
            {
                if (count-- == 0)
                    return;
                source.Add(item);
            }
        }

        public static List<T> With<T>(this List<T> list, T element)
        {
            list.Add(element);
            return list;
        }
        
        public static T FromAdded<T>(this List<T> list, T element)
        {
            list.Add(element);
            return element;
        }

        public static bool TryGet<T>(this IList<T> source, int index, out T item)
        {
            var outOfBounds = source.OutOfBounds(index);
            item = outOfBounds ? default : source[index];
            return !outOfBounds;
        }

        public static bool OutOfBounds<T>(this IList<T> source, int index) =>
            index < 0 || index >= source.Count;
        
        private static readonly System.Random random = new();
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n-- > 1)
            {
                var k = random.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }
    }
}