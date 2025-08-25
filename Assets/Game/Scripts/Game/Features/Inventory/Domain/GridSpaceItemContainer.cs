using System.Collections.Generic;
using System.Linq;
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

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems() => 
            _items.Select(n => (n.Value, GetItemPositionNormalized(n.Key)));

        private Vector2 GetItemPositionNormalized(string id)
        {
            var item = _gridSpace.GetItem(id);
            return GridPositionToNormalized(item.Size, item.Position, _gridSpace.Size);
        }

        private Vector2 GridPositionToNormalized(Vector2Int itemSize, Vector2Int gridPosition, Vector2Int gridSize) => 
            (Vector2)gridPosition / gridSize + (Vector2)itemSize / 2;

        public override ICommand GetExtractItemCommand(string itemId, IExtractedItemCaretaker caretaker) => 
            new ExtractedItemCommand(this, itemId, caretaker);

        public override ItemPlacementResult GetItemPlacement(ItemPlacementProposition proposition)
        {
            var position = GetPosition(proposition);
            return new ItemPlacementResult(GetIsPossible(proposition), position.x, position.y, GetReplacement(proposition));

            IItem GetReplacement(ItemPlacementProposition itemPlacementProposition)
            {
                throw new System.NotImplementedException();
            }

            Vector2Int GetPosition(ItemPlacementProposition proposition)
            {
                throw new System.NotImplementedException();
            }

            bool GetIsPossible(ItemPlacementProposition proposition)
            {
                throw new System.NotImplementedException();
            }
        }


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