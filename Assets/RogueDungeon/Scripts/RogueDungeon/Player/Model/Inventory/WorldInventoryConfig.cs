using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryConfig : ScriptableObject
    {
        [field: SerializeField] public float OpenAnimationDuration { get; private set; }
        [field: SerializeField] public AnimationClip OpenAnimation { get; private set; }
    }
}