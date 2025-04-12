using System;

namespace Common.Animations
{
    public interface IAnimation
    {
        float Progress { get; }
        bool IsFinished { get; }
        event Action<string> OnEvent;
        void Play();
        void TickNormalizedTime(float delta);
    }
}