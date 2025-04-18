using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    public class BlockAnimationsConfig : ItemAnimationsConfig
    {
        [field: HideLabel, BoxGroup(nameof(RaiseBlockAnimation)), SerializeField] public ItemAnimationConfig RaiseBlockAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(HoldBlockAnimation)), SerializeField] public ItemAnimationConfig HoldBlockAnimation {get; private set;}
        [field: HideLabel, BoxGroup(nameof(AbsorbBlockImpactAnimation)), SerializeField] public ItemAnimationConfig AbsorbBlockImpactAnimation {get; private set;}
    }
}