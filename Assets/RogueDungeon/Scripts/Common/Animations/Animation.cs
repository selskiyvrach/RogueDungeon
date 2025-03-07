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
        private readonly Ticker _ticker = new();
        private float _timePassed;
        
        protected abstract float Duration { get; }
        protected abstract IEnumerable<(float time, string name)> Events { get; }
        
        public bool IsFinished => _timePassed >= Duration;
        public event Action<string> OnEvent;

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
            ApplyAnimation(Mathf.Clamp01(_timePassed / Duration));
            
            foreach (var (time, name) in Events ?? Enumerable.Empty<(float, string)>())
            {
                Assert.IsTrue(time >= 0 && time < Duration);
                if(time > lastFrameTime && time < _timePassed)
                    OnEvent?.Invoke(name);
            }
            
            if(IsFinished)
                Stop();
        }

        public virtual void Stop() => 
            _ticker.Stop();

        protected abstract void ApplyAnimation(float timeNormalized);
    }
}