namespace Common.Animations
{
    public readonly struct AnimationData
    {
        public readonly string Name;
        public readonly float Duration;

        public AnimationData(string name, float duration)
        {
            Name = name;
            Duration = duration;
        }
    }
}