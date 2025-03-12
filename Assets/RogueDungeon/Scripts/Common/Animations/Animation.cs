using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Animations
{
    public abstract class Animation : IAnimation
    {
        private readonly AnimationConfigWithDuration _config;
        private float _timePassed;

        protected abstract AnimationEvent[] Events { get; }
        public float Progress => Mathf.Clamp01(_timePassed / _config.Duration);
        public bool IsFinished => _timePassed >= _config.Duration;
        public event Action<string> OnEvent;

        protected Animation(AnimationConfigWithDuration config) => 
            _config = config;

        public virtual void Play()
        {
            _timePassed = 0;
            ApplyAnimation(0);
        }

        public void Tick(float timeDelta)
        {
            if(IsFinished)
                return;
            
            var lastFrameTime = _timePassed;
            _timePassed += timeDelta;
            ApplyAnimation(Mathf.Clamp01(_timePassed / _config.Duration));
            
            foreach (var e in Events ?? Enumerable.Empty<AnimationEvent>())
            {
                Assert.IsTrue(e.Time >= 0 && e.Time <= _config.Duration);
                if(e.Time > lastFrameTime && e.Time < _timePassed)
                    OnEvent?.Invoke(e.Name);
            }
        }

        protected abstract void ApplyAnimation(float timeNormalized);
    }
}