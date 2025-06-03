using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Items.Domain;
using Game.Features.Items.Domain.Moves;
using Libs.Animations;
using Libs.Movesets;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Items.Infrastructure.Repository
{
    public class MapItemConfig : ItemConfig
    {
        [field: BoxGroup("Durations"), SerializeField] public float RaiseMapDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float LowerMapDuration { get; private set; } = .25f;
        [field: BoxGroup("Durations"), SerializeField] public float HoldMapAnimationDuration { get; private set; } = 1;
        [field: HideLabel, BoxGroup(nameof(RaiseMapAnimation)), SerializeField] public TransformAnimationConfig RaiseMapAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(HoldMapAnimation)), SerializeField] public TransformAnimationConfig HoldMapAnimation {get; private set;}

        public override Type ItemType => typeof(Map);
        protected override IEnumerable<TransitionPicker> TransitionsFromIdle => base.TransitionsFromIdle.Append(new TransitionPicker(Names.MAP_RAISE, canInterrupt: true));

        public override IEnumerable<MoveCreationArgs> MovesCreationArgs => base.MovesCreationArgs.Concat(new MoveCreationArgs[]
        {
            new(Names.MAP_RAISE, typeof(RaiseMapMove), RaiseMapAnimation, new []{new TransitionPicker(Names.MAP_HOLD)}),
            new(Names.MAP_HOLD, typeof(HoldMapMove), HoldMapAnimation, new TransitionPicker[]
            {
                new(Names.MAP_LOWER, canInterrupt: true),
            }),
            new(Names.MAP_LOWER, typeof(LowerMapMove), _returnToIdleAnimationConfig, new TransitionPicker[]{new(Names.IDLE)}),
        });
    }
}