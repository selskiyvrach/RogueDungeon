using System;
using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Commands;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class FreeSpaceItemContainer : ItemContainer
    {
        private readonly List<(Vector2 position, IItem item)> _items = new();

        public FreeSpaceItemContainer(ContainerId id) : base(id)
        {
        }

        public void PlaceItem(IItem item, Vector2 localPositionNormalized) => 
            _items.Add((localPositionNormalized, item));

        public FreeSpaceItemPlacementEvaluation GetPlaceItemEvaluation(IItem item, Vector2 localPositionNormalized) => 
            new(isPossible: true, resultPos: localPositionNormalized);

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker) => 
            new ExtractItemCommand(this, itemId, caretaker);

        public override void AcceptVisitor(IContainerVisitor visitor) => 
            visitor.Visit(this);

        private IItem ExtractItem(string itemId, out Vector2 position)
        {
            for (var i = 0; i < _items.Count; i++)
            {
                if (_items[i].item.Id != itemId) 
                    continue;
                
                position = _items[i].position;
                _items.RemoveAt(i);
                return null;
            }
            throw new Exception($"Item with id {itemId} not found");
        }

        private class ExtractItemCommand : ItemOperationCommand
        {
            private readonly IExtractedItemCaretaker _caretaker;
            private readonly FreeSpaceItemContainer _container;
            private readonly string _itemId;
            
            private Vector2 _itemPosition;
            private IItem _item;

            public ExtractItemCommand(FreeSpaceItemContainer container, string itemId, IExtractedItemCaretaker caretaker) : base(container)
            {
                _container = container;
                _itemId = itemId;
                _caretaker = caretaker;
            }

            protected override void ExecuteInternal() => 
                _caretaker.SetItem(_item = _container.ExtractItem(_itemId ,out _itemPosition));

            protected override void UndoInternal()
            {
                _caretaker.RemoveItem(_item);
                _container.PlaceItem(_item, _itemPosition);
            }
        }
    }
}