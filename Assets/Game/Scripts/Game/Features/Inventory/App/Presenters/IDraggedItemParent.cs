using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IDraggedItemParent
    {
        Vector3 WorldPosition { get; }
        void SetItem(IItemView item);
        void SetScreenPosition(Vector2 position);
        void RemoveItem();
    }
}