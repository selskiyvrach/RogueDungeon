using System;
using System.Collections.Generic;
using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Items.Model.Moves;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items.Model.Configs
{
    public abstract class ItemConfig : ScriptableObject, IMoveSetConfig
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Vector2Int Size { get; private set; } = new(1, 1);
        
        [field: BoxGroup("Durations"), SerializeField] public float IdleAnimationDuration { get; private set; } = 1f;
        [field: BoxGroup("Durations"), SerializeField] public float UnsheathDuration { get; private set; } = .5f;

        [HideLabel, BoxGroup(nameof(_returnToIdleAnimationConfig)), SerializeField] protected TransformAnimationConfig _returnToIdleAnimationConfig;
        [BoxGroup("Animations"), SerializeField] private BasicAnimationsConfig _basicAnimationsConfig;

        public string FirstMoveId => Names.UNSHEATH;

        public abstract Type ItemType { get; }

        protected virtual IEnumerable<TransitionPicker> TransitionsFromIdle => new TransitionPicker[] { new(Names.SHEATH, canInterrupt: true), };

        public virtual IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new(Names.UNSHEATH, typeof(UnsheathMove), _basicAnimationsConfig.UnsheathAnimation, new []{new TransitionPicker(Names.IDLE)}),
            new(Names.IDLE, typeof(ItemIdleMove), _basicAnimationsConfig.IdleAnimation, TransitionsFromIdle),
            new(Names.SHEATH, typeof(SheathMove), _basicAnimationsConfig.SheathAnimation, Array.Empty<TransitionPicker>()),
        };
    }
}