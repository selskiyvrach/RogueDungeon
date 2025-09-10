using UnityEngine.UI;

namespace Libs.Utils.Unity
{
    public static class GraphicExtensions
    {
        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            var c = graphic.color;
            c.a = alpha;
            graphic.color = c;
        }
    }
}