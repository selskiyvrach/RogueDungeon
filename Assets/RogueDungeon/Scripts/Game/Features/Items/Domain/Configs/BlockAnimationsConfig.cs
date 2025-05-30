using Libs.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Items.Domain.Configs
{
    public class BlockAnimationsConfig : TransformAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(RaiseBlockAnimation)), SerializeField] public TransformAnimationConfig RaiseBlockAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(HoldBlockAnimation)), SerializeField] public TransformAnimationConfig HoldBlockAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(AbsorbBlockImpactAnimation)), SerializeField] public TransformAnimationConfig AbsorbBlockImpactAnimation {get; private set;}
    }
}