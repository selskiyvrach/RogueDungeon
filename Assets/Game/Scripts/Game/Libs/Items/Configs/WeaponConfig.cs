using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public class WeaponConfig : BlockingItemConfig
    {
        [field: BoxGroup("Stamina"), SerializeField] public float Damage { get; private set; } = 10;
        [field: BoxGroup("Damage"), SerializeField] public float PoiseDamage { get; private set; } = 10;
        [field: BoxGroup("Damage"), SerializeField] public float AttackStaminaCost { get; private set; } = 10;
        [field: BoxGroup("Durations"), SerializeField] public float PrepareAttackDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float AttackExecuteDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float AttackRecoveryDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float TransitionBetweenAttacksDuration { get; private set; } = .25f;

        [BoxGroup("Animations"), SerializeField] private AttackAnimationsConfig _attackAnimations;

        public override Type ItemType => typeof(Weapon);
        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new(MoveNames.FIRST_ATTACK_PREPARE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            // first attack
            new(MoveNames.FIRST_ATTACK_PREPARE, _attackAnimations.FirstAttackPrepareAnimation, new TransitionPicker[]{new (MoveNames.FIRST_ATTACK_EXECUTE)}, MoveTypeIds.ATTACK_PREPARE),
            new(MoveNames.FIRST_ATTACK_EXECUTE, _attackAnimations.FirstAttackExecuteAnimation, new TransitionPicker[]  
            {
                new (MoveNames.FIRST_TO_SECOND_ATTACK_TRANSITION),
                new (MoveNames.FIRST_ATTACK_RECOVER),
            }, MoveTypeIds.ATTACK_EXECUTE),
            new(MoveNames.FIRST_ATTACK_RECOVER, _returnToIdleAnimationConfig, new TransitionPicker[]{ new (MoveNames.IDLE)}, MoveTypeIds.ATTACK_RECOVER),
            new(MoveNames.FIRST_TO_SECOND_ATTACK_TRANSITION, _attackAnimations.FirstToSecondAttackTransitionAnimation, new TransitionPicker[]
            {
                new (MoveNames.SECOND_ATTACK_EXECUTE),
            }, MoveTypeIds.ATTACK_TRANSITION),
            
            // second attack
            new(MoveNames.SECOND_ATTACK_EXECUTE, _attackAnimations.SecondAttackExecuteAnimation, new TransitionPicker[]
            {
                new (MoveNames.SECOND_ATTACK_RECOVER),
            }, MoveTypeIds.ATTACK_EXECUTE),
            new(MoveNames.SECOND_ATTACK_RECOVER, _returnToIdleAnimationConfig, new TransitionPicker[]{ new (MoveNames.IDLE)}, MoveTypeIds.ATTACK_RECOVER),
        });
    }
}