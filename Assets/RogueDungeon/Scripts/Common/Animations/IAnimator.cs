using System;

namespace Common.Animations
{
    public interface IAnimator
    {
        event Action<string> OnEvent;
        void Play(AnimationData animationData);
        void Play(LoopedAnimationData loopedAnimationData);
    }
}