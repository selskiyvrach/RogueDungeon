namespace Game.Features.Inventory.Infrastructure.View
{
    public interface IInventoryInfoProvider
    {
        InventoryItemView GetSlotItem(SlotType slotType);
    }
}