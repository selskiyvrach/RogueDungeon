using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public sealed record SlotItemPlacement(IItem Item) : ItemPlacement(Item)
    {
    }
}