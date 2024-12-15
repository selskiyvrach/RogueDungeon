using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.UtilsDotNet
{
    public static class EnumExtensions
    {
        public static T ThrowIfNone<T>(this T target) where T : struct, Enum
        {
            if (target.ToString() == "None")
                throw new Exception("Item has value of None");
            return target;
        }

        public static IEnumerable<T> GetValues<T>(this T target) where T : struct, Enum => 
            Enum.GetValues(typeof(T)).Cast<T>();
    }
}