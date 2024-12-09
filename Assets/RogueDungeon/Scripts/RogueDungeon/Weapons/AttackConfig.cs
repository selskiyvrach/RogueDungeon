using System;
using RogueDungeon.Animations;
using RogueDungeon.Collisions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    [Serializable]
    public class AttackConfig : IAttackTimingsProvider, IAttackAnimationsProvider
    {
        // OVERRIDES
        [field: TitleGroup("Overrides"),HorizontalGroup("Overrides/Row1"), LabelText("Damage"), SerializeField] public bool OverrideDamage { get; private set; }
        [field: HorizontalGroup("Overrides/Row1"), LabelText("HitMask"), SerializeField] public bool OverrideHitMask { get; private set; }
        [field: HorizontalGroup("Overrides/Row1"), LabelText("Timings"), SerializeField] public bool OverrideTimings { get; private set; }
        

        // DAMAGE AND HITMASK
        [field: ShowIf(nameof(OverrideDamage)), TitleGroup("Damage"), HorizontalGroup("Damage/1", width: 0.4f), SerializeField] public float Damage { get; private set; } = 10;
        [field: ShowIf(nameof(OverrideHitMask)), SerializeField, HorizontalGroup("Damage/1", width: 0.6f)] public Positions HitMask { get; private set; } = Positions.EnemyFrontCenter;
        
        
        // TIMINGS
        [field: ShowIf(nameof(OverrideTimings)), TitleGroup("Timings"),HorizontalGroup("Timings/Row1"), LabelText("Prepare"), SerializeField] public float PrepareAnimationDuration { get; private set; }
        [field: ShowIf(nameof(OverrideTimings)), HorizontalGroup("Timings/Row1"), LabelText("Execute"), SerializeField] public float ExecuteAnimationDuration { get; private set; }
        [field: ShowIf(nameof(OverrideTimings)), HorizontalGroup("Timings/Row1"), LabelText("Finish"), SerializeField] public float FinishAnimationDuration { get; private set; }

        
        // ANIMATIONS
        [field: SerializeField, TitleGroup(nameof(PrepareAnimation)), HideLabel] public AnimationClip PrepareAnimation { get; private set; }
        [field: SerializeField, TitleGroup(nameof(ExecuteAnimation)), HideLabel] public AnimationClip ExecuteAnimation {get; private set; }
        [field: SerializeField, TitleGroup(nameof(FinishAnimation)), HideLabel] public AnimationClip FinishAnimation {get; private set; }
        

        float IAttackTimingsProvider.GetPrepareDuration() => 
            PrepareAnimationDuration;

        float IAttackTimingsProvider.GetExecuteDuration() => 
            ExecuteAnimationDuration;

        float IAttackTimingsProvider.GetFinishDuration() => 
            FinishAnimationDuration;
    }
}