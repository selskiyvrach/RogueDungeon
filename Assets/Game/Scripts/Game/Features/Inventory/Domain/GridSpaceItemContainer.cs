using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Commands;
using Libs.GridSpace;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class GridSpaceItemContainer : ItemContainer
    {
        private readonly GridSpace _gridSpace;
        private readonly Dictionary<string, IItem> _items = new();

        public GridSpaceItemContainer(int columns, int rows, ContainerId id) : base(id) => 
            _gridSpace = new GridSpace(columns, rows);

        public GridSpaceItemPlacementEvaluation GetItemPlacementEvaluation(IItem item, Vector2 localPositionNormalized) => 
            new(isPossible: true, replacedItemId: item.Id, resultLocalPosNormalized: localPositionNormalized);

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker) => 
            new ExtractedItemCommand(this, itemId, caretaker);

        public override void AcceptVisitor(IContainerVisitor visitor) => 
            visitor.Visit(this);

        private IItem ExtractItem(string itemId, out Vector2Int coords)
        {
            var item = _gridSpace.GetItem(itemId);
            if(item.Equals(default(GridSpaceItem)))
                throw new KeyNotFoundException($"Item with id {itemId} not found");
            
            coords = item.Position;
            var result = _items[itemId];
            _items.Remove(itemId);
            return result;
        }

        private void PlaceItem(IItem item, Vector2Int coords)
        {
            _gridSpace.Insert(new GridSpaceItem(item.Id, item.Size, coords));
            _items[item.Id] = item;
        }

        private class ExtractedItemCommand : ItemOperationCommand
        {
            private readonly GridSpaceItemContainer _container;
            private readonly string _itemId;
            private readonly IExtractedItemCaretaker _caretaker;
            
            private Vector2Int _itemPosition;
            private IItem _item;

            public ExtractedItemCommand(GridSpaceItemContainer container, string itemId, IExtractedItemCaretaker caretaker) : base(container)
            {
                _container = container;
                _itemId = itemId;
                _caretaker = caretaker;
            }

            protected override void ExecuteInternal() => 
                _caretaker.SetItem(_item = _container.ExtractItem(_itemId, out _itemPosition));

            protected override void UndoInternal()
            {
                _caretaker.RemoveItem(_item);
                _container.PlaceItem(_item, _itemPosition);
            }
        }
    }
}