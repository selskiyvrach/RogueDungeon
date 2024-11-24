using System;

namespace Common.DotNetUtils
{
    public static class EnumExtensions
    {
        public static T ThrowIfNone<T>(this T target)
        {
            if (target.ToString() == "None")
                throw new Exception("Item has value of None");
            return target;
        }
    }
}