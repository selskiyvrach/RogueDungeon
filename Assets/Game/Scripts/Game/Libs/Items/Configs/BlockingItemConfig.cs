using System.Collections.Generic;
using System.Linq;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public abstract class BlockingItemConfig : ItemConfig
    {
        [field: BoxGroup("Durations"), SerializeField] public float BlockStaminaCostMultiplier { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float RaiseBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float LowerBlockDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float HoldBlockAnimationDuration { get; private set; } = 1;
        [field: BoxGroup("Durations"), SerializeField] public float BlockImpactAbsorptionDuration { get; private set; } = .25f;
        
        [BoxGroup("Animations"), SerializeField] private BlockAnimationsConfig _blockAnimationsConfig;

        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new TransitionPicker(MoveNames.BLOCK_RAISE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            new(MoveNames.BLOCK_RAISE, _blockAnimationsConfig.RaiseBlockAnimation, new []{new TransitionPicker(MoveNames.BLOCK_HOLD)}),
            new(MoveNames.BLOCK_HOLD, _blockAnimationsConfig.HoldBlockAnimation, new TransitionPicker[]
            {
                new(MoveNames.BLOCK_ABSORB_IMPACT, canInterrupt: true),
                new(MoveNames.BLOCK_LOWER, canInterrupt: true),
            }),
            new(MoveNames.BLOCK_ABSORB_IMPACT, _blockAnimationsConfig.AbsorbBlockImpactAnimation, new TransitionPicker[]{new(MoveNames.BLOCK_HOLD)}),
            new(MoveNames.BLOCK_LOWER, _returnToIdleAnimationConfig, new TransitionPicker[]{new(MoveNames.IDLE)}),
        });
    }
}