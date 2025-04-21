using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Player.Model.Attacks;
using RogueDungeon.Player.Model.Behaviours.Hands;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public abstract class ItemConfig : ScriptableObject, IMoveSetConfig
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: BoxGroup("Durations"), SerializeField] public float IdleAnimationDuration { get; private set; } = 1f;
        [field: BoxGroup("Durations"), SerializeField] public float UnsheathDuration { get; private set; } = .5f;
        
        [HideLabel, BoxGroup(nameof(_returnToIdleAnimationConfig)), SerializeField] protected TransformAnimationConfig _returnToIdleAnimationConfig;
        [BoxGroup("Animations"), SerializeField] private BasicAnimationsConfig _basicAnimationsConfig;
        
        public string FirstMoveId => Names.UNSHEATH;

        protected virtual IEnumerable<TransitionPicker> TransitionsFromIdle => new TransitionPicker[] { new(Names.SHEATH, canInterrupt: true), };

        public virtual IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new(Names.UNSHEATH, typeof(UnsheathMove), _basicAnimationsConfig.UnsheathAnimation, new []{new TransitionPicker(Names.IDLE)}),
            new(Names.IDLE, typeof(ItemIdleMove), _basicAnimationsConfig.IdleAnimation, TransitionsFromIdle),
            new(Names.SHEATH, typeof(SheathMove), _basicAnimationsConfig.SheathAnimation, Array.Empty<TransitionPicker>()),
        };
    }
}