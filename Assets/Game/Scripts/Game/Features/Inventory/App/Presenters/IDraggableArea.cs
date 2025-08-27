using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IDraggableArea
    {
        Vector3 ScreenToWorldPoint(Vector2 screenPoint);
    }
}