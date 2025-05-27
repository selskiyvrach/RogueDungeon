using System.Collections.Generic;
using System.Linq;
using Common.MoveSets;
using RogueDungeon.Items.Model.Moves;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items.Model.Configs
{
    public abstract class BlockingItemConfig : ItemConfig
    {
        [field: BoxGroup("Durations"), SerializeField] public float BlockStaminaCostMultiplier { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float RaiseBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float LowerBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float HoldBlockAnimationDuration { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float BlockImpactAbsorptionDuration { get; private set; } = .25f;
        
        [BoxGroup("Animations"), SerializeField] private BlockAnimationsConfig _blockAnimationsConfig;

        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new TransitionPicker(Names.BLOCK_RAISE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            new(Names.BLOCK_RAISE, typeof(ItemRaiseBlockMove), _blockAnimationsConfig.RaiseBlockAnimation, new []{new TransitionPicker(Names.BLOCK_HOLD)}),
            new(Names.BLOCK_HOLD, typeof(ItemHoldBlockMove), _blockAnimationsConfig.HoldBlockAnimation, new TransitionPicker[]
            {
                new(Names.BLOCK_ABSORB_IMPACT, canInterrupt: true),
                new(Names.BLOCK_LOWER, canInterrupt: true),
            }),
            new(Names.BLOCK_ABSORB_IMPACT, typeof(ItemAbsorbBlockImpactMove), _blockAnimationsConfig.AbsorbBlockImpactAnimation, new TransitionPicker[]{new(Names.BLOCK_HOLD)}),
            new(Names.BLOCK_LOWER, typeof(ItemLowerBlockMove), _returnToIdleAnimationConfig, new TransitionPicker[]{new(Names.IDLE)}),
        });
    }
}