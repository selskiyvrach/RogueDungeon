namespace Libs.Utils.DotNet
{
    public static class Random
    {
        public static float PlusMinusPercent(float source, float percent) => 
            source + UnityEngine.Random.Range(source - source * percent, source + source * percent);
        
        public static bool FlipACoin() =>
            UnityEngine.Random.Range(0, 2) == 0;
    }
}