using System;
using UnityEngine;

namespace Libs.Utils.DotNet
{
    public static class ObjectExtensions
    {
        public static T ThrowIfNull<T>(this T obj, string message = null) where T : class => 
            obj ?? throw new Exception(message ?? $"Object of type {typeof(T)} is null");

        public static string TypeName(this object obj) 
            => obj.GetType().Name;
    }

    public static class RectExtensions
    {
        public static float AspectRatio(this Rect rect) => 
            rect.width / rect.height;
    }
}