using System;
using System.Collections.Generic;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public abstract class ItemConfig : ScriptableObject, IItemConfig, IHandheldItemViewConfig, IHandheldItemConfig, IMoveSetConfig
    {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Vector2Int Size { get; private set; } = new(1, 1);
        [field: SerializeField] public string ItemTypeId { get; private set; }
        [field: BoxGroup("Durations"), SerializeField] public float IdleAnimationDuration { get; private set; } = 1f;
        [field: BoxGroup("Durations"), SerializeField] public float UnsheathDuration { get; private set; } = .5f;

        [HideLabel, BoxGroup(nameof(_returnToIdleAnimationConfig)), SerializeField] protected TransformAnimationConfig _returnToIdleAnimationConfig;
        [BoxGroup("Animations"), SerializeField] private BasicAnimationsConfig _basicAnimationsConfig;

        public string FirstMoveId => MoveNames.UNSHEATH;

        public abstract Type ItemType { get; }

        protected virtual IEnumerable<TransitionPicker> TransitionsFromIdle => new TransitionPicker[] { new(MoveNames.SHEATH, canInterrupt: true), };
        
        public virtual IEnumerable<MoveCreationArgs> MovesCreationArgs => new MoveCreationArgs[]
        {
            new(MoveNames.UNSHEATH, _basicAnimationsConfig.UnsheathAnimation, new []{new TransitionPicker(MoveNames.IDLE)}),
            new(MoveNames.IDLE, _basicAnimationsConfig.IdleAnimation, TransitionsFromIdle),
            new(MoveNames.SHEATH, _basicAnimationsConfig.SheathAnimation, Array.Empty<TransitionPicker>()),
        };
    }
}