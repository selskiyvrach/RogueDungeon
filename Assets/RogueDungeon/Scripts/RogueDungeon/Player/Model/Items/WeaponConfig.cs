using System.Collections.Generic;
using System.Linq;
using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Player.Model.Attacks;
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

        [BoxGroup("Animations"), SerializeField] private AttackAnimationsConfig _attackAnimations;

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
            new(Names.FIRST_ATTACK_RECOVER, typeof(ItemRecoverAttackMove), _attackAnimations.FirstAttackRecoverAnimation, new TransitionPicker[]{ new (Names.IDLE)}),
            new(Names.FIRST_TO_SECOND_ATTACK_TRANSITION, typeof(ItemTransitionBetweenAttacksMove), _attackAnimations.FirstToSecondAttackTransitionAnimation, new TransitionPicker[]
            {
                new (Names.SECOND_ATTACK_EXECUTE),
            }),
            
            // second attack
            new(Names.SECOND_ATTACK_EXECUTE, typeof(ItemExecuteAttackMove), _attackAnimations.SecondAttackExecuteAnimation, new TransitionPicker[]
            {
                new (Names.SECOND_TO_FIRST_ATTACK_TRANSITION),
                new (Names.SECOND_ATTACK_RECOVER),
            }),
            new(Names.SECOND_ATTACK_RECOVER, typeof(ItemRecoverAttackMove), _attackAnimations.SecondAttackRecoverAnimation, new TransitionPicker[]{ new (Names.IDLE)}),
            new(Names.SECOND_TO_FIRST_ATTACK_TRANSITION, typeof(ItemTransitionBetweenAttacksMove), _attackAnimations.SecondToFirstAttackTransitionAnimation, new TransitionPicker[]
            {
                new (Names.FIRST_ATTACK_EXECUTE),
            }),
        });
    }
}