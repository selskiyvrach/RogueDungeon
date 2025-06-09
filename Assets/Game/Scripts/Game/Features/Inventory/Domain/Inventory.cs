using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public event Action<Hand> OnCurrentHandheldItemChanged;
        
        public Inventory(InventoryConfig config)
        {
            foreach (var slot in config.Slots)
            {
                _slots.Add(slot, null);
                
                var cyclableSlot = slot.Hand.ToCyclableItemsGroup();
                if(cyclableSlot.IsNone())
                    continue;
                _cyclableSlotQueues.TryAdd(cyclableSlot, new Queue<SlotId>());
                _cyclableSlotQueues[cyclableSlot].Enqueue(slot);
            }
        }

        public IEnumerable<KeyValuePair<Vector2Int, IItem>> GetBackpackItems() => 
            _backpackItems;

        public void AddBackpackItem(Vector2Int position, IItem item) => 
            _backpackItems.Add(position, item);
        
        public void RemoveBackpackItem(Vector2Int position) => 
            _backpackItems.Remove(position);

        public void Equip(IItem item, SlotId slotId)
        {
            Assert.IsNull(_slots[slotId]);
            _slots[slotId] = item;
            RaiseEventsOnSlotContentChanged(slotId);
        }

        public IItem Unequip(SlotId slotId)
        {
            Assert.IsNotNull(_slots[slotId]);
            var result = _slots[slotId];
            _slots[slotId] = null;
            RaiseEventsOnSlotContentChanged(slotId);
            return result;
        }

        public void CycleItemsInGroup(CyclableItemsGroup group)
        {
            group.ThrowIfNone();
            var prevItem = GetCurrentItemFromGroup(group);
            var attemptsLeft = _cyclableSlotQueues[group].Count;
            do
                _cyclableSlotQueues[group].RequeueTopOne();
            while (--attemptsLeft > 0 || GetCurrentItemFromGroup(group) is not null);
            var resultItem = GetCurrentItemFromGroup(group);
            if(prevItem != resultItem)
                OnCurrentHandheldItemChanged?.Invoke(group.ToHand());
        }

        public IItem GetCurrentItemFromGroup(CyclableItemsGroup group)
        {
            group.ThrowIfNone();
            return GetItem(_cyclableSlotQueues[group].Peek());
        }
        
        public IHandheldItem GetCurrentHandheldItem(Hand hand) =>
            (IHandheldItem)_slots[GetCurrentHandItemSlotId(hand)];

        public IItem GetItem(SlotId slotId) => 
            _slots.GetValueOrDefault(slotId);

        private void RaiseEventsOnSlotContentChanged(SlotId slotId)
        {
            if(!slotId.Hand.IsNone() && GetCurrentHandItemSlotId(slotId.Hand) == slotId)
                OnCurrentHandheldItemChanged?.Invoke(slotId.Hand);
        }

        private SlotId GetCurrentHandItemSlotId(Hand hand) => 
            _cyclableSlotQueues[hand.ToCyclableItemsGroup()].Peek();
    }
}