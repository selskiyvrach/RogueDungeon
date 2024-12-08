using System;
using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    [Serializable]
    public class AttackConfig : IAttackTimingsProvider, IAttackAnimationsProvider
    {
        [field: SerializeField] public float PrepareDuration { get; private set; } = .5f;
        [field: SerializeField] public AnimationConfig PrepareAnimation {get; private set; }
        
        [field: SerializeField] public float ExecuteDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationConfig ExecuteAnimation {get; private set; }
        
        [field: SerializeField] public float FinishDuration {get; private set;}= .5f;
        [field: SerializeField] public AnimationConfig FinishAnimation {get; private set; }

        float IAttackTimingsProvider.GetPrepareDuration() => 
            PrepareDuration;

        float IAttackTimingsProvider.GetExecuteDuration() => 
            ExecuteDuration;

        float IAttackTimingsProvider.GetFinishDuration() => 
            FinishDuration;
    }
}