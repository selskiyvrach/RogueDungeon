using Libs.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Features.Items.Domain.Configs
{
    public class AttackAnimationsConfig : TransformAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(FirstAttackPrepareAnimation)), SerializeField] public TransformAnimationConfig FirstAttackPrepareAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(FirstAttackExecuteAnimation)), SerializeField] public TransformAnimationConfig FirstAttackExecuteAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(FirstToSecondAttackTransitionAnimation)), SerializeField] public TransformAnimationConfig FirstToSecondAttackTransitionAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SecondAttackExecuteAnimation)), SerializeField] public TransformAnimationConfig SecondAttackExecuteAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SecondToFirstAttackTransitionAnimation)), SerializeField] public TransformAnimationConfig SecondToFirstAttackTransitionAnimation {get; private set;}
    }
}