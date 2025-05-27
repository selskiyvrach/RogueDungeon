using UnityEngine;

namespace Inventory.View
{
    public class WorldInventoryConfig : ScriptableObject
    {
        [field: SerializeField] public float OpenAnimationDuration { get; private set; }
        [field: SerializeField] public AnimationClip OpenAnimation { get; private set; }
    }
}