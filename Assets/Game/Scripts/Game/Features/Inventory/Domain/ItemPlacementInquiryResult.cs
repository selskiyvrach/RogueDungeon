using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public record ItemPlacementInquiryResult(bool IsPossible, float XNormalized, float YNormalized, IItem ReplacedItem)
    {
        public bool IsPossible { get; } = IsPossible;
        public float XNormalized { get; } = XNormalized;
        public float YNormalized { get; } = YNormalized; 
        public IItem ReplacedItem { get; } = ReplacedItem;
    }
}