using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemProjection
    {
        void SetPosition(Vector3 position);
        void SetSize(Vector2 size);
        void SetIsLegal(bool isLegal);
    }

    public class ItemProjectionInquirer : IContainerVisitor
    {
        private readonly IItemProjection _itemProjection;

        // public ItemProjectionInquirer(IItemProjection itemProjection) => 
        //     _itemProjection = itemProjection;

        public void Visit(GridSpaceItemContainer container)
        {
            var evaluation = container.GetItemPlacementEvaluation(null, Vector2.zero);
            _itemProjection.SetIsLegal(evaluation.IsPossible);
            // _itemProjection.SetSize(evaluation.);
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