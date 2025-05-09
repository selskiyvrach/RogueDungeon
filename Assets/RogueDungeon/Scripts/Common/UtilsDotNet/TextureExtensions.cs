using UnityEngine;

namespace Common.UtilsDotNet
{
    public static class TextureExtensions
    {
        public static float AspectRatio(this Texture texture) => 
            texture.width / (float)texture.height;
    }
}