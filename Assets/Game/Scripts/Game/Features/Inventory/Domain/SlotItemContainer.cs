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

        protected override ItemPlacement GetItemPlacementFromProjection(IItem item, ItemPlacementInquiryResult placementInquiry) => 
            new SlotItemPlacement(item);

        protected override void PlaceItemInternal(ItemPlacement placement)
        {
            if(_item != null)
                throw new InvalidOperationException("Slot is already occupied");
            _item = (ISlotableItem)placement.Item ?? throw new ArgumentNullException(nameof(placement.Item));
        }

        protected override IItem ExtractItemInternal(string itemId)
        {
            if(itemId.IsNullOrEmpty() || itemId != _item.Id)
                throw new ArgumentNullException(nameof(itemId));
            var result = _item;
            _item = null;
            return result;
        }

        public override ItemPlacementInquiryResult GetItemPlacementInquiry(ItemPlacementProposition proposition) =>
            new(IsPossible: proposition.Item is ISlotableItem slotable && slotable.SlotCategory == _slotCategory, .5f, .5f, _item);

        public ISlotableItem PeekItem() => 
            _item;
    }
}