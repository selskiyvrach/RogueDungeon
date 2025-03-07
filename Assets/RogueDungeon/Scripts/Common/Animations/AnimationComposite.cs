using System;
using System.Linq;
using Common.Time;

namespace Common.Animations
{
    public class AnimationComposite : IAnimation
    {
        private readonly Ticker _ticker = new();
        private readonly IAnimation[] _animations;

        public bool IsFinished => _animations.All(a => a.IsFinished);
        public event Action<string> OnEvent;
        
        public AnimationComposite(IAnimation[] animations) => 
            _animations = animations;

        public void Play()
        {
            _ticker.Start(CheckFinishedAndStopIfSo);
            foreach (var animation in _animations)
            {
                animation.OnEvent += RaiseEvent;
                animation.Play();
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var animation in _animations) 
                animation.Tick(deltaTime);
        }

        public void Stop()
        {
            _ticker.Stop();
            foreach (var animation in _animations) 
                animation.OnEvent -= RaiseEvent;
        }

        private void RaiseEvent(string name) => 
            OnEvent?.Invoke(name);

        private void CheckFinishedAndStopIfSo(float _)
        {
            if(IsFinished)
                Stop();
        }
    }
}