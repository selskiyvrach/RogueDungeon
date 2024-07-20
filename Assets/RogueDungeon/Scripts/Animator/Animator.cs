using RogueDungeon.Time;
using UnityEngine;

namespace RogueDungeon.Animator
{
    public interface IAnimationsProvider
    {
        Animation Get(string name);
    }

    public class Animation
    {
        private AnimationClip _clip;
        private GameObject _target;
        private float _duration;
        
        public void Apply(float time)
        {
            var timeNormalized = time / _duration;
            _clip.SampleAnimation(_target, timeNormalized);
        }
    }

    public class Animator
    {
        private readonly IAnimationsProvider _animationsProvider;
        private readonly ITime _time;
        private Animation _currentAnimation;
        private float _playTime;

        public Animator(IAnimationsProvider animationsProvider, ITime time)
        {
            _animationsProvider = animationsProvider;
            _time = time;
        }

        public void Tick()
        {
            _playTime += _time.TimeDelta;
            _currentAnimation.Apply(_playTime);
        }

        public void PlayAnimation(string animationName)
        {
            _playTime = 0;
            _currentAnimation = _animationsProvider.Get(animationName);
        }
    }
}