using UnityEngine;

namespace Game.Features.Inventory.View
{
    public interface IItemViewInfo
    {
        Vector2Int Size { get; }
        Sprite Sprite { get; }
        int InstanceId { get; }
        bool IsEquippableIntoSlotType(SlotType slotType);
    }
}