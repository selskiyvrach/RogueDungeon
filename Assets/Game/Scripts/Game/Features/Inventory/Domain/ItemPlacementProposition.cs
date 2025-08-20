using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public record ItemPlacementProposition(float XNormalized, float YNormalized, IItem Item)
    {
        public IItem Item { get; } = Item;
        public float YNormalized { get; } = YNormalized;
        public float XNormalized { get; } = XNormalized;
    }
}