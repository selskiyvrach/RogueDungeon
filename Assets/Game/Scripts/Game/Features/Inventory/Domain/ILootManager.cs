using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public interface ILootManager
    {
        ItemContainer GetRoomLootContainer(Vector2Int roomId);
    }
}