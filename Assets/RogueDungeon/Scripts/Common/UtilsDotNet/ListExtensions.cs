using System;
using System.Collections.Generic;

namespace Common.UtilsDotNet
{
    public static class QueueExtensions
    {
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (var item in items) 
                queue.Enqueue(item);
        }
    }

    public static class ListExtensions
    {
        public static T Random<T>(this IList<T> source) => 
            source[UnityEngine.Random.Range(0, source.Count)];

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
        
        private static readonly Random random = new();
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