using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public abstract record ItemPlacement(IItem Item)
    {
        public IItem Item { get; } = Item;
    }
}