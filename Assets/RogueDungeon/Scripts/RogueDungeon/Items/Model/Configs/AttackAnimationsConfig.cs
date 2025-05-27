using Common.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items.Model.Configs
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