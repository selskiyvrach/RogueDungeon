using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Animations
{
    public abstract class Animation : IAnimation
    {
        private readonly AnimationConfig _config;
        private float _progress;
        protected abstract AnimationEvent[] Events { get; }

        public float Progress
        {
            get => _progress;
            private set => _progress = Mathf.Clamp01(value);
        }

        public bool IsFinished => Progress >= 1f;
        public event Action<string> OnEvent;

        protected Animation(AnimationConfig config) => 
            _config = config;

        public virtual void Play()
        {
            Progress = 0f;
            ApplyAnimation(0);
        }

        public void TickNormalizedTime(float delta)
        {
            if(IsFinished)
                return;
            
            var lastFrameTime = Progress;
            Progress += delta;
            ApplyAnimation(Progress);
            
            foreach (var e in Events ?? Enumerable.Empty<AnimationEvent>())
            {
                if(e.Time > lastFrameTime && e.Time <= Progress)
                    OnEvent?.Invoke(e.Name);
            }
        }

        protected abstract void ApplyAnimation(float timeNormalized);
    }
}