using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.View
{
    public interface IDraggableArea
    {
        Vector3 ScreenToWorldPoint(Vector2 screenPoint);
    }
}