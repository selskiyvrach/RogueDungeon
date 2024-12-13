using System;
using Common.Animations;
using Common.UnityUtils;
using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class TestEnvironment : MonoBehaviour, IComboCounter, IComboInfo, IInput, IAnimator, IDurations, IControlState
    {
        [Serializable]
        public class Durations : SerializableDictionary<Duration, float>
        {
        }
        
        [SerializeField] private AttackDirection[] _attackDirections;
        [SerializeField] private Durations _durations;
        private IAnimator _animatorImplementation;

        int IComboCounter.Count { get; set; }
        AttackDirection[] IComboInfo.Directions => _attackDirections;
        public bool IsInUncancellableAnimation { get; set; }
        bool IInput.TryConsume(Input input)
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
        }
        float IDurations.Get(Duration type) => 
            _durations[type];

        bool IControlState.Is(AbleTo ableTo)
        {
            return true;
        }

        event Action<string> IAnimator.OnEvent
        {
            add => _animatorImplementation.OnEvent += value;
            remove => _animatorImplementation.OnEvent -= value;
        }

        void IAnimator.Play(AnimationData animationData) => 
            _animatorImplementation.Play(animationData);

        void IAnimator.Play(LoopedAnimationData loopedAnimationData) => 
            _animatorImplementation.Play(loopedAnimationData);
    }
}