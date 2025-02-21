using UnityEngine;

namespace Common.Unity
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            var cos = Mathf.Cos(rad);
            var sin = Mathf.Sin(rad);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }
        
        public static Vector2Int Round(this Vector2 v) => 
            new(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
    }
} 