namespace Common.Animations
{
    public readonly struct AnimationEvent
    {
        public readonly string Name { get; }

        public AnimationEvent(string name) => 
            Name = name;
    }
}