using System;
using System.Collections.Generic;
using System.Linq;
using Common.Time;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Animations
{
    public abstract class Animation : IAnimation
    {
        private readonly AnimationConfigWithDuration _config;
        private readonly Ticker _ticker = new();
        private float _timePassed;

        protected abstract AnimationEvent[] Events { get; }
        public bool IsFinished => _timePassed >= _config.Duration;
        public event Action<string> OnEvent;

        protected Animation(AnimationConfigWithDuration config) => 
            _config = config;

        public virtual void Play()
        {
            _timePassed = 0;
            _ticker.Start(Tick);
            ApplyAnimation(0);
        }

        private void Tick(float timeDelta)
        {
            Assert.IsFalse(IsFinished);
            var lastFrameTime = _timePassed;
            _timePassed += timeDelta;
            ApplyAnimation(Mathf.Clamp01(_timePassed / _config.Duration));
            
            foreach (var e in Events ?? Enumerable.Empty<AnimationEvent>())
            {
                Assert.IsTrue(e.Time >= 0 && e.Time <= _config.Duration);
                if(e.Time > lastFrameTime && e.Time < _timePassed)
                    OnEvent?.Invoke(e.Name);
            }
            
            if(IsFinished)
                Stop();
        }

        public virtual void Stop() => 
            _ticker.Stop();

        protected abstract void ApplyAnimation(float timeNormalized);
    }
}