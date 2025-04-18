using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class AttackAnimationsConfig : ItemAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(FirstAttackPrepareAnimation)), SerializeField] public ItemAnimationConfig FirstAttackPrepareAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(FirstAttackExecuteAnimation)), SerializeField] public ItemAnimationConfig FirstAttackExecuteAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(FirstToSecondAttackTransitionAnimation)), SerializeField] public ItemAnimationConfig FirstToSecondAttackTransitionAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SecondAttackExecuteAnimation)), SerializeField] public ItemAnimationConfig SecondAttackExecuteAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SecondToFirstAttackTransitionAnimation)), SerializeField] public ItemAnimationConfig SecondToFirstAttackTransitionAnimation {get; private set;}
    }
}