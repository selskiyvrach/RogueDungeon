using System;
using System.Collections.Generic;
using System.Linq;
using Common.UnityUtils;
using UnityEngine;

namespace Common.Animations
{
    public class Animator : MonoBehaviour, IAnimator
    {
        [Serializable] private class Animations : SerializableDictionary<string, AnimationClip> { }
        [SerializeField] private Animations _animations;
        [SerializeField] private UnityEngine.Animator _animator;
        
        private static readonly int SpeedHash = UnityEngine.Animator.StringToHash("speed");
        private Dictionary<string, int> _hashes = new();

        public event Action<string> OnEvent;

        private void Awake() => 
            _hashes = _animations.ToDictionary(n => n.Key, n => UnityEngine.Animator.StringToHash(n.Key));

        private void ReceiveEvent(string value) => 
            OnEvent?.Invoke(value);

        public void Play(AnimationData animationData)
        {
            SetSpeed(_animations[animationData.Name].length / animationData.Duration);
            SetTrigger(animationData.Name);
        }

        public void Play(LoopedAnimationData loopedAnimationData)
        {
            SetSpeed(loopedAnimationData.Speed);
            SetTrigger(loopedAnimationData.Name);
        }

        private void SetTrigger(string animationName) => 
            _animator.SetTrigger(_hashes[animationName]);

        private void SetSpeed(float value) => 
            _animator.SetFloat(SpeedHash, value);
    }
}