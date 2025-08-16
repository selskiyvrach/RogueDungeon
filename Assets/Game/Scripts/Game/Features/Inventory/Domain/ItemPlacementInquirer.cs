using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class ItemPlacementInquirer : IContainerVisitor
    {
        public void Visit(GridSpaceItemContainer container)
        {
            var evaluation = container.GetItemPlacementEvaluation(null, Vector2.zero);
        }

        public void Visit(FreeSpaceItemContainer container)
        {
            var evaluation = container.GetPlaceItemEvaluation(null, Vector2.zero);
        }

        public void Visit(SlotItemContainer container)
        {
            var evaluation = container.GetItemPlacementEvaluation(null);
        }
    }
}