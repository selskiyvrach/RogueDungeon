using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public interface ILootContainersLocator
    {
        ItemContainer GetRoomLootContainer(Vector2Int roomId);
    }
}