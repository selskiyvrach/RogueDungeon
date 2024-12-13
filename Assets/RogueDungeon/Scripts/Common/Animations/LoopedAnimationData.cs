namespace Common.Animations
{
    public readonly struct LoopedAnimationData
    {
        public readonly string Name;
        public readonly float Speed;

        public LoopedAnimationData(string name, float speed)
        {
            Name = name;
            Speed = speed;
        }
    }
}