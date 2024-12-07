using System;
using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    [Serializable]
    public class AttackConfig : IAttackTimingsProvider, IAttackAnimationsConfig
    {
        [field: SerializeField] public float PrepareDuration { get; private set; } = .5f;
        [field: SerializeField] public AnimationConfig PrepareAnimation {get; private set; }
        
        [field: SerializeField] public float ExecuteDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationConfig ExecuteAnimation {get; private set; }
        
        [field: SerializeField] public float FinishDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationConfig FinishAnimation {get; private set; }

        public float GetPrepareDuration() => 
            PrepareDuration;

        public float GetExecuteDuration() => 
            ExecuteDuration;

        public float GetFinishDuration() => 
            FinishDuration;
    }
}