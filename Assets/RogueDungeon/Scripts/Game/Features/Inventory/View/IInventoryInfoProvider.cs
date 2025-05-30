namespace Game.Features.Inventory.View
{
    public interface IInventoryInfoProvider
    {
        InventoryItemView GetSlotItem(SlotType slotType);
    }
}