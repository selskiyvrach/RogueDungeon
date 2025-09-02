using System.Collections.Generic;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class LootManager : ILootManager
    {
        private readonly Dictionary<Vector2Int, ItemContainer> _lootAreas = new();

        public ItemContainer GetRoomLootContainer(Vector2Int roomId)
        {
            if(!_lootAreas.ContainsKey(roomId))
                _lootAreas.Add(roomId, new FreeSpaceItemContainer(ContainerId.Ground0));
            return _lootAreas[roomId];
        }
    }
}