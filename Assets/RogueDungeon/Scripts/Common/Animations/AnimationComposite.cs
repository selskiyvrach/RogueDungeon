using System;
using System.Linq;

namespace Common.Animations
{
    public class AnimationComposite : IAnimation
    {
        private readonly IAnimation[] _animations;

        public bool IsFinished => _animations.All(a => a.IsFinished);
        public event Action<string> OnEvent;
        
        public AnimationComposite(IAnimation[] animations) => 
            _animations = animations;

        public void Play()
        {
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
            if(IsFinished)
                Stop();
        }

        public void Stop()
        {
            foreach (var animation in _animations) 
                animation.OnEvent -= RaiseEvent;
        }

        private void RaiseEvent(string name) => 
            OnEvent?.Invoke(name);
    }
}