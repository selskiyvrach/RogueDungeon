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

        public void PlaceItem(ISlotableItem item)
        {
            if(_item != null)
                throw new InvalidOperationException("Slot is already occupied");
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }

        private ISlotableItem ExtractItem()
        {
            var result = _item;
            _item = null;
            return result;
        }

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems()
        {
            _items[0] = (_item, Vector2.one / 2);
            return _items;
        }

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker)
        {
            if(itemId.IsNullOrEmpty() || itemId != _item.Id)
                throw new ArgumentNullException(nameof(itemId));
            return new ExtractItemCommand(this, _item, caretaker);
        }

        public override ItemPlacementResult GetItemPlacement(ItemPlacementProposition proposition) =>
            new(IsPossible: proposition.Item is ISlotableItem slotable && slotable.SlotCategory == _slotCategory, .5f, .5f, _item);

        public ISlotableItem PeekItem() => 
            _item;

        private class ExtractItemCommand : ItemOperationCommand
        {
            private readonly IExtractedItemCaretaker _extractedItemCaretaker;
            private readonly SlotItemContainer _container;
            private readonly ISlotableItem _item;

            public ExtractItemCommand(SlotItemContainer container, ISlotableItem item, IExtractedItemCaretaker extractedItemCaretaker) : base(container)
            {
                _container = container;
                _item = item;
                _extractedItemCaretaker = extractedItemCaretaker;
            }

            protected override void ExecuteInternal()
            {
                if(_container.PeekItem().Id != _item.Id)
                    throw new InvalidOperationException("Invalid item");
                _extractedItemCaretaker.SetItem(_container.ExtractItem());
            }

            protected override void UndoInternal()
            {
                if(_container.PeekItem() != null)
                    throw new InvalidOperationException("Slot is already occupied");
                _extractedItemCaretaker.RemoveItem(_item);
                _container.PlaceItem(_item);
            }
        }
    }
}