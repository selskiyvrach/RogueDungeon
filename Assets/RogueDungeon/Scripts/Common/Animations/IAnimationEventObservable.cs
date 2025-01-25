using UniRx;

namespace Common.Animations
{
    public interface IAnimationEventObservable
    {
        ISubject<AnimationEvent> OnEvent { get; }
    }
}