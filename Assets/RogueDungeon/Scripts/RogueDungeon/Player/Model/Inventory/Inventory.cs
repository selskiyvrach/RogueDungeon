using System;
using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Model.Inventory
{
    public class Inventory
    {
        private readonly Dictionary<SlotType, IItem> _slots = new();
        private readonly Dictionary<Vector2Int, IItem> _backpackItems = new();
        
        public Inventory()
        {
            foreach (var name in Enum.GetNames(typeof(SlotType))) 
                _slots.Add(Enum.Parse<SlotType>(name), null);
        }

        public IEnumerable<KeyValuePair<Vector2Int, IItem>> GetBackpackItems() => _backpackItems;

        public void AddBackpackItem(Vector2Int position, IItem item) => 
            _backpackItems.Add(position, item);
        
        public void RemoveBackpackItem(Vector2Int position) => 
            _backpackItems.Remove(position);

        public void Equip(IItem item, SlotType slotType)
        {
            Assert.IsNull(_slots[slotType]);
            _slots[slotType] = item;
        }

        public IItem Unequip(SlotType slotType)
        {
            Assert.IsNotNull(_slots[slotType]);
            var result = _slots[slotType];
            _slots[slotType] = null;
            return result;
        }

        public IItem GetItem(SlotType slotType) => 
            _slots.GetValueOrDefault(slotType);
    }
}