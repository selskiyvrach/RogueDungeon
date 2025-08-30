using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IDraggableArea
    {
        float CellSize { get; }
        Vector3 ScreenToWorldPoint(Vector2 screenPoint);
    }
}