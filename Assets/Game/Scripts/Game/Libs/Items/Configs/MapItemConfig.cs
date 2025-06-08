using System;
using System.Collections.Generic;
using System.Linq;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Libs.Items.Configs
{
    public class MapItemConfig : ItemConfig
    {
        [field: BoxGroup("Durations"), SerializeField] public float RaiseMapDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float LowerMapDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float HoldMapAnimationDuration { get; private set; } = 1;
        [field: HideLabel, BoxGroup(nameof(RaiseMapAnimation)), SerializeField] public TransformAnimationConfig RaiseMapAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(HoldMapAnimation)), SerializeField] public TransformAnimationConfig HoldMapAnimation {get; private set;}

        public override Type Type => typeof(Map);
        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new TransitionPicker(MoveNames.MAP_RAISE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            new(MoveNames.MAP_RAISE, RaiseMapAnimation, new []{new TransitionPicker(MoveNames.MAP_HOLD)}),
            new(MoveNames.MAP_HOLD, HoldMapAnimation, new TransitionPicker[]
            {
                new(MoveNames.MAP_LOWER, canInterrupt: true),
            }),
            new(MoveNames.MAP_LOWER, _returnToIdleAnimationConfig, new TransitionPicker[]{new(MoveNames.IDLE)}),
        });
    }
}