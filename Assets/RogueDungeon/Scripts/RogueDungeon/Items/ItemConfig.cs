using UnityEngine;

namespace RogueDungeon.Items
{
    public class ItemConfig : ScriptableObject
    {
        [field: SerializeField] public HandHeldItemPresenter Prefab { get; private set; }
    }
}