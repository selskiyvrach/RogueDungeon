using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Items.Domain.Moves;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Items.Domain.Configs
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
        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new(Names.FIRST_ATTACK_PREPARE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            // first attack
            new(Names.FIRST_ATTACK_PREPARE, typeof(ItemPrepareAttackMove), _attackAnimations.FirstAttackPrepareAnimation, new TransitionPicker[]{new (Names.FIRST_ATTACK_EXECUTE)}),
            new(Names.FIRST_ATTACK_EXECUTE, typeof(ItemExecuteAttackMove), _attackAnimations.FirstAttackExecuteAnimation, new TransitionPicker[]  
            {
                new (Names.FIRST_TO_SECOND_ATTACK_TRANSITION),
                new (Names.FIRST_ATTACK_RECOVER),
            }),
            new(Names.FIRST_ATTACK_RECOVER, typeof(ItemRecoverAttackMove), _returnToIdleAnimationConfig, new TransitionPicker[]{ new (Names.IDLE)}),
            new(Names.FIRST_TO_SECOND_ATTACK_TRANSITION, typeof(ItemTransitionBetweenAttacksMove), _attackAnimations.FirstToSecondAttackTransitionAnimation, new TransitionPicker[]
            {
                new (Names.SECOND_ATTACK_EXECUTE),
            }),
            
            // second attack
            new(Names.SECOND_ATTACK_EXECUTE, typeof(ItemExecuteAttackMove), _attackAnimations.SecondAttackExecuteAnimation, new TransitionPicker[]
            {
                new (Names.SECOND_ATTACK_RECOVER),
            }),
            new(Names.SECOND_ATTACK_RECOVER, typeof(ItemRecoverAttackMove), _returnToIdleAnimationConfig, new TransitionPicker[]{ new (Names.IDLE)}),
        });
    }
}