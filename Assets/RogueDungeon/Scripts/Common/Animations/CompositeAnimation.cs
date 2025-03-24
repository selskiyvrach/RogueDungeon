using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Animations
{
    public class CompositeAnimation : IAnimation
    {
        private readonly IAnimation[] _animations;

        public float Progress => _animations.Min(n => n.Progress);
        public bool IsFinished => _animations.All(n => n.IsFinished);
        public event Action<string> OnEvent;

        public CompositeAnimation(IEnumerable<IAnimation> animations) => 
            _animations = animations.ToArray();

        public void Play()
        {
            foreach (var animation in _animations)
            {
                animation.OnEvent -= RaiseEvent;
                animation.OnEvent += RaiseEvent;
                animation.Play();
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var animation in _animations) 
                animation.Tick(deltaTime);
        }

        private void RaiseEvent(string obj) => 
            OnEvent?.Invoke(obj);
    }
}