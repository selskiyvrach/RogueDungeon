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
        private readonly (IItem item, Vector2 pos)[] _items = new (IItem item, Vector2 pos)[1];
        private ISlotableItem _item;

        public SlotItemContainer(SlotCategory slotCategory, ContainerId id) : base(id) => 
            _slotCategory = slotCategory;

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems()
        {
            if(_item == null)
                return Array.Empty<(IItem item, Vector2 posNormalized)>();
            _items[0] = (_item, Vector2.one / 2);
            return _items;
        }

        protected override void PlaceItemInternal(IItem item, Vector2 posNormalized)
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

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, Vector2 posNormalized) =>
            new(IsPossible: item is ISlotableItem slotable && slotable.SlotCategory == _slotCategory, Vector2.one / 2, _item);

        public ISlotableItem PeekItem() => 
            _item;
    }
}