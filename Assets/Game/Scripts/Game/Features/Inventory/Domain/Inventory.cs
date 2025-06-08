using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.Domain
{
    public class Inventory
    {
        private readonly Dictionary<SlotId, IItem> _slots = new();
        private readonly Dictionary<CyclableItemsGroup, Queue<SlotId>> _cyclableSlotQueues = new();
        private readonly Dictionary<Vector2Int, IItem> _backpackItems = new();
        
        public Inventory(InventoryConfig config) => 
            _slots.AddRangeOfKeys(config.Slots);

        public IEnumerable<KeyValuePair<Vector2Int, IItem>> GetBackpackItems() => _backpackItems;

        public void AddBackpackItem(Vector2Int position, IItem item) => 
            _backpackItems.Add(position, item);
        
        public void RemoveBackpackItem(Vector2Int position) => 
            _backpackItems.Remove(position);

        public void Equip(IItem item, SlotId slotId)
        {
            Assert.IsNull(_slots[slotId]);
            _slots[slotId] = item;
        }

        public IItem Unequip(SlotId slotId)
        {
            Assert.IsNotNull(_slots[slotId]);
            var result = _slots[slotId];
            _slots[slotId] = null;
            return result;
        }

        public void CycleItemsInGroup(CyclableItemsGroup group)
        {
            group.ThrowIfNone();
            var attemptsCount = _cyclableSlotQueues[group].Count;
            do
                _cyclableSlotQueues[group].RequeueTopOne();
            while (--attemptsCount > 0 || GetCurrentItemFromGroup(group) is not null);
        }

        public IItem GetCurrentItemFromGroup(CyclableItemsGroup group)
        {
            group.ThrowIfNone();
            return GetItem(_cyclableSlotQueues[group].Peek());
        }

        public IItem GetItem(SlotId slotId) => 
            _slots.GetValueOrDefault(slotId);
    }
}