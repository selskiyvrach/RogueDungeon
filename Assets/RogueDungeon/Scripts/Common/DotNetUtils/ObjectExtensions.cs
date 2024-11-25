using System;

namespace Common.DotNetUtils
{
    public static class ObjectExtensions
    {
        public static T ThrowIfNull<T>(this T obj) where T : class => 
            obj ?? throw new Exception("Object is null");
    }
}