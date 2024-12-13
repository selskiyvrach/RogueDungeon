using System;
using Common.UnityUtils;
using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class TestEnvironment : MonoBehaviour, IComboCounter, IComboInfo, IInput, IAnimator, IDurations, IControlState
    {
        [SerializeField] private AttackDirection[] _attackDirections;
        [SerializeField] private Durations _durations;
        
        int IComboCounter.Count { get; set; }
        AttackDirection[] IComboInfo.Directions => _attackDirections;
        public bool IsInUncancellableAnimation { get; set; }
        bool IInput.TryConsume(Input input)
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
        }
        void IAnimator.Play(PlayOptions playOptions)
        {
            
        }
        float IDurations.Get(Duration type) => 
            _durations[type];

        bool IControlState.Is(AbleTo ableTo)
        {
            return true;
        }
    }

    [Serializable]
    public class Durations : SerializableDictionary<Duration, float>
    {
    }
}