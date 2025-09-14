using System;
using System.Collections.Generic;
using Game.Libs.Items;
using Game.Libs.Items.Factory;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    // a presenter/animation controller
    // looks for the items dropped in a presenters registry, calls play dropped animation
    
    public class LootManager : ILootContainersLocator, ILootDropper
    {
        private readonly IItemFactory _itemsFactory;
        private readonly ILootConfigsRepository _lootConfigsRepository;
        private readonly Dictionary<Vector2Int, ItemContainer> _lootAreas = new();
        private readonly List<IItem> _lastDroppedItems = new();
        
        public event Action<Vector2Int, List<IItem>> OnItemsDropped;

        public LootManager(ILootConfigsRepository lootConfigsRepository, IItemFactory itemsFactory)
        {
            _lootConfigsRepository = lootConfigsRepository;
            _itemsFactory = itemsFactory;
        }

        public ItemContainer GetRoomLootContainer(Vector2Int roomId)
        {
            if(!_lootAreas.ContainsKey(roomId))
                _lootAreas.Add(roomId, new FreeSpaceItemContainer(ContainerId.Ground0));
            return _lootAreas[roomId];
        }

        public void DropLoot(string lootId, Vector2Int roomId)
        {
            _lastDroppedItems.Clear();
            var lootArea = GetRoomLootContainer(roomId);
            foreach (var item in _lootConfigsRepository.GetLootConfig(lootId).Items)
            {
                var created = _itemsFactory.Create(item);
                _lastDroppedItems.Add(created);
                lootArea.PlaceItem(created, PositionNormalized.Random);
            }

            OnItemsDropped?.Invoke(roomId, _lastDroppedItems);
        }
    }
}