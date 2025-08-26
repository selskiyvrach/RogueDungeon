using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IDraggedItemParent
    {
        void SetItem(IItemView item);
        void SetScreenPosition(Vector2 position);
        void RemoveItem();
    }
}