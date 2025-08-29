using System;
using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Commands;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class SlotItemContainer : ItemContainer
    {
        private readonly SlotCategory _slotCategory;
        private readonly (IItem item, PositionNormalized pos)[] _items = new (IItem item, PositionNormalized pos)[1];
        private ISlotableItem _item;

        public SlotItemContainer(SlotCategory slotCategory, ContainerId id) : base(id) => 
            _slotCategory = slotCategory;

        public override IEnumerable<(IItem item, PositionNormalized position)> GetItems()
        {
            if(_item == null)
                return Array.Empty<(IItem item, PositionNormalized posNormalized)>();
            _items[0] = (_item, PositionNormalized.Center);
            return _items;
        }

        protected override void PlaceItemInternal(IItem item, PositionNormalized position)
        {
            if(_item != null)
                throw new InvalidOperationException("Slot is already occupied");
            
            _item = (ISlotableItem)item ?? throw new ArgumentNullException(nameof(item));
        }

        public override bool ContainsItem(IItem item) => 
            item == _item;

        protected override void RemoveItemInternal(IItem item)
        {
            if(item == null || item != _item)
                throw new ArgumentException(nameof(item));
            _item = null;
        }

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, PositionNormalized posNormalized) =>
            new(IsPossible: item is ISlotableItem slotable && slotable.SlotCategory == _slotCategory, PositionNormalized.Center, _item);

        public ISlotableItem PeekItem() => 
            _item;
    }
}