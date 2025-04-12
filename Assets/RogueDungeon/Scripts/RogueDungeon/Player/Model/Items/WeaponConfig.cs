using Common.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class WeaponConfig : ItemConfig
    {
        [field: BoxGroup("Stamina"), SerializeField] public float Damage { get; private set; } = 10;
        [field: BoxGroup("Damage"), SerializeField] public float PoiseDamage { get; private set; } = 10;
        [field: BoxGroup("Damage"), SerializeField] public float AttackStaminaCost { get; private set; } = 10;
        [field: BoxGroup("Durations"), SerializeField] public float PrepareAttackDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float AttackExecuteDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float AttackRecoveryDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float TransitionBetweenAttacksDuration { get; private set; } = .25f;
        
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _firstAttackPrepareAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _firstAttackExecuteAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _firstAttackRecoverAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _firstToSecondAttackTransitionAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _secondAttackExecuteAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _secondAttackRecoverAnimation;
        [BoxGroup("Attacks"), SerializeField] private AnimationConfigPicker _secondToFirstAttackTransitionAnimation;
    }
}