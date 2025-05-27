using Inventory.Shared;
using UnityEngine;

namespace Inventory.View
{
    public interface IItemViewInfo
    {
        Vector2Int Size { get; }
        Sprite Sprite { get; }
        int InstanceId { get; }
        bool IsEquippableIntoSlotType(SlotType slotType);
    }
}