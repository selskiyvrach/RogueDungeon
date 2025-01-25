namespace Common.Animations
{
    public interface IAnimationEventRaiser
    {
        void Raise(AnimationEvent @event);
    }
}