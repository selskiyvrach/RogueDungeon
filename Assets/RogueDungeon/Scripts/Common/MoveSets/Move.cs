namespace Common.MoveSets
{
    public class Move : IMove
    {
        public string AnimationName { get; }
        public float Duration { get; }
        public IMove[] Transitions { get; }

        protected Move(string animationName, float duration, IMove[] transitions)
        {
            AnimationName = animationName;
            Duration = duration;
            Transitions = transitions;
        }
    }
}