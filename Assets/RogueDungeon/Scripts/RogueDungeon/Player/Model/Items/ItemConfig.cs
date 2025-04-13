using System;
using System.Collections.Generic;
using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Player.Model.Attacks;
using RogueDungeon.Player.Model.Behaviours.Hands;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class ItemConfig : ScriptableObject, IMoveSetConfig
    {
        public static class Names
        {
            public const string UNSHEATH = "unsheath";
            public const string IDLE = "Idle";
            public const string SHEATH = "sheath";

            public const string BLOCK_RAISE = "block_raise";
            public const string BLOCK_HOLD = "block_hold";
            public const string BLOCK_LOWER = "block_lower";
            public const string BLOCK_ABSORB_IMPACT = "block_absorb_impact";
            
            public const string FIRST_ATTACK_PREPARE = "first_attack_prepare";
            public const string FIRST_ATTACK_EXECUTE = "first_attack_execute";
            public const string FIRST_TO_SECOND_ATTACK_TRANSITION = "first_to_second_attack_transition";
            public const string FIRST_ATTACK_RECOVER = "first_attack_recover";
            
            public const string SECOND_ATTACK_EXECUTE = "second_attack_execute";
            public const string SECOND_TO_FIRST_ATTACK_TRANSITION = "second_to_first_attack_transition";
            public const string SECOND_ATTACK_RECOVER = "second_attack_recover";
        }

        [SerializeField] private ItemMoveSetConfig _baseConfig;

        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: BoxGroup("Durations"), SerializeField] public float IdleAnimationDuration { get; private set; } = 1f;
        [field: BoxGroup("Durations"), SerializeField] public float BlockStaminaCostMultiplier { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float RaiseBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float LowerBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float HoldBlockAnimationDuration { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float BlockImpactAbsorptionDuration { get; private set; } = .25f;

        [BoxGroup("Common"), SerializeField] private ItemAnimationConfig _idleAnimation;
        [BoxGroup("Common"), SerializeField] private ItemAnimationConfig _sheathAnimation;
        [BoxGroup("Common"), SerializeField] private ItemAnimationConfig _unsheatAnimation;
        [BoxGroup("Block"), SerializeField] private ItemAnimationConfig _raiseBlockAnimation;
        [BoxGroup("Block"), SerializeField] private ItemAnimationConfig _lowerBlockAnimation;
        [BoxGroup("Block"), SerializeField] private ItemAnimationConfig _holdBlockAnimation;
        [BoxGroup("Block"), SerializeField] private ItemAnimationConfig _absorbBlockImpactAnimation;
        
        public string FirstMoveId => Names.UNSHEATH;
        
        public virtual IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new(Names.UNSHEATH, typeof(UnsheathMove), _unsheatAnimation, new []{new TransitionPicker(Names.IDLE)}),
            new(Names.IDLE, typeof(ItemIdleMove), _idleAnimation, TransitionsFromIdle),
            new(Names.SHEATH, typeof(SheathMove), _sheathAnimation, Array.Empty<TransitionPicker>()),
            
            new(Names.BLOCK_RAISE, typeof(ItemRaiseBlockMove), _raiseBlockAnimation, new []{new TransitionPicker(Names.BLOCK_HOLD)}),
            new(Names.BLOCK_HOLD, typeof(ItemHoldBlockMove), _holdBlockAnimation, new TransitionPicker[]
            {
                new(Names.BLOCK_ABSORB_IMPACT, canInterrupt: true), 
                new(Names.BLOCK_LOWER, canInterrupt: true),
            }),
            new(Names.BLOCK_ABSORB_IMPACT, typeof(ItemAbsorbBlockImpactMove), _absorbBlockImpactAnimation, new TransitionPicker[]{new(Names.BLOCK_HOLD)}),
            new(Names.BLOCK_LOWER, typeof(ItemLowerBlockMove), _lowerBlockAnimation, new TransitionPicker[]{new(Names.IDLE)}),
        };

        protected virtual IEnumerable<TransitionPicker> TransitionsFromIdle => new TransitionPicker[]
        {
            new(Names.BLOCK_RAISE, canInterrupt: true),
            new(Names.SHEATH, canInterrupt: true),
        };
    }
}