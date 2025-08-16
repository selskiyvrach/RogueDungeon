using System;
using Game.Libs.Items;
using Libs.Commands;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class SlotItemContainer : ItemContainer
    {
        private readonly SlotCategory _slotCategory;
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

        public SlotContainerItemPlacementEvaluation GetItemPlacementEvaluation(IItem item)
        {
            var isPossible = _slotCategory == _item.SlotCategory;
            var replacedItem = isPossible ? _item : null;
            return new SlotContainerItemPlacementEvaluation(isPossible, resultLocalPosNormalized: Vector2.one / 2,
                replacedItem?.Id);
        }

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker)
        {
            if(itemId.IsNullOrEmpty() || itemId != _item.Id)
                throw new ArgumentNullException(nameof(itemId));
            return new ExtractItemCommand(this, _item, caretaker);
        }

        public override void AcceptVisitor(IContainerVisitor visitor) => 
            visitor.Visit(this);

        public ISlotableItem PeekItem() => 
            _item;

        private class ExtractItemCommand : ICommand
        {
            private readonly IExtractedItemCaretaker _extractedItemCaretaker;
            private readonly SlotItemContainer _container;
            private readonly ISlotableItem _item;

            public ExtractItemCommand(SlotItemContainer container, ISlotableItem item, IExtractedItemCaretaker extractedItemCaretaker)
            {
                _container = container;
                _item = item;
                _extractedItemCaretaker = extractedItemCaretaker;
            }

            public void Execute()
            {
                if(_container.PeekItem().Id != _item.Id)
                    throw new InvalidOperationException("Invalid item");
                _extractedItemCaretaker.SetItem(_container.ExtractItem());
            }

            public void Undo()
            {
                if(_container.PeekItem() != null)
                    throw new InvalidOperationException("Slot is already occupied");
                _extractedItemCaretaker.RemoveItem(_item);
                _container.PlaceItem(_item);
            }
        }
    }
}