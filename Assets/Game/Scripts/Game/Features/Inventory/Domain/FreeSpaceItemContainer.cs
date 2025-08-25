using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using Libs.Commands;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class FreeSpaceItemContainer : ItemContainer
    {
        private readonly List<(IItem item, Vector2 position)> _items = new();

        public FreeSpaceItemContainer(ContainerId id) : base(id)
        {
        }

        private void PlaceItem(IItem item, Vector2 localPositionNormalized) => 
            _items.Add((item, localPositionNormalized));

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems() => 
            _items;

        public override void PlaceItem(ItemPlacement placement) => 
            PlaceItem(placement.Item, ((FreeSpaceItemPlacement)placement).Position);

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker) => 
            new ExtractItemCommand(this, itemId, caretaker);

        public override ItemPlacementResult GetItemPlacement(ItemPlacementProposition proposition) => 
            new(IsPossible: true, proposition.XNormalized, proposition.YNormalized, ReplacedItem: null);

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