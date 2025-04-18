using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class BasicAnimationsConfig : ItemAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(IdleAnimation)), SerializeField] public ItemAnimationConfig IdleAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(SheathAnimation)), SerializeField] public ItemAnimationConfig SheathAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(UnsheathAnimation)), SerializeField] public ItemAnimationConfig UnsheathAnimation {get; private set;}
    }
}