using Inventory.Shared;

namespace Inventory.View
{
    public interface IInventoryInfoProvider
    {
        InventoryItemView GetSlotItem(SlotType slotType);
    }
}