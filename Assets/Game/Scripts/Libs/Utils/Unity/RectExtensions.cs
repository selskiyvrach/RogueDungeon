using UnityEngine;

namespace Libs.Utils.Unity
{
    public static class RectExtensions
    {
        public static float AspectRatio(this Rect rect) => 
            rect.width / rect.height;
    }
}