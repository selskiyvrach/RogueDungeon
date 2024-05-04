using System;
using System.Collections.Generic;

namespace RogueDungeon.Utils
{
    public static class IListUtils
    {
        public static T Random<T>(this IList<T> source) => 
            source[UnityEngine.Random.Range(0, source.Count)];
        
        public static bool TryGet<T>(this IList<T> source, int index, out T item)
        {
            var outOfBounds = source.IndexOutOfBounds(index);
            item = outOfBounds ? default : source[index];
            return !outOfBounds;
        }

        public static bool IndexOutOfBounds<T>(this IList<T> source, int index) =>
            index < 0 || index >= source.Count;
     
        
        private static readonly Random random = new Random();
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