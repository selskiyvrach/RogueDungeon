using System;

namespace Common.Animations
{
    public interface IAnimation
    {
        bool IsFinished { get; }
        event Action<string> OnEvent;
        void Play();
        void Tick(float deltaTime);
    }
}