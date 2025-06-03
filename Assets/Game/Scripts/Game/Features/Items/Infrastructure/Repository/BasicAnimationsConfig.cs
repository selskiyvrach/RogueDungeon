using Libs.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Items.Infrastructure.Repository
{
    public class BasicAnimationsConfig : TransformAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(IdleAnimation)), SerializeField] public TransformAnimationConfig IdleAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SheathAnimation)), SerializeField] public TransformAnimationConfig SheathAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(UnsheathAnimation)), SerializeField] public TransformAnimationConfig UnsheathAnimation {get; private set;}
    }
}