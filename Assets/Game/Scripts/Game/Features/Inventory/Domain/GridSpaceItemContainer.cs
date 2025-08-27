using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
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

        protected override void PlaceItemInternal(ItemPlacement placement) => 
            PlaceItem(placement.Item, ((GridSpaceItemPlacement)placement).Position);

        protected override ItemPlacement GetItemPlacementFromProjection(IItem item, ItemPlacementInquiryResult placementInquiry) => 
            new GridSpaceItemPlacement(item, NormalizedToGridPosition(new Vector2(placementInquiry.XNormalized, placementInquiry.YNormalized), item.Size, _gridSpace.Size));

        private Vector2 GetItemPositionNormalized(string id)
        {
            var item = _gridSpace.GetItem(id);
            return GridPositionToNormalized(item.Size, item.Position, _gridSpace.Size);
        }

        private Vector2 GridPositionToNormalized(Vector2Int itemSize, Vector2Int gridPosition, Vector2Int gridSize) => 
            (Vector2)gridPosition / gridSize + (Vector2)itemSize / 2 / gridSize;

        private Vector2Int NormalizedToGridPosition(Vector2 normalizedPosition, Vector2Int itemSize, Vector2Int gridSize)
        {
            var pos = normalizedPosition * gridSize - itemSize / 2;
            return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        }

        protected override IItem ExtractItemInternal(string itemId)
        {
            var item = _items[itemId];
            if (!_gridSpace.Remove(itemId) || !_items.Remove(itemId))
                throw new InvalidOperationException();
            return item;
        }

        public override ItemPlacementInquiryResult GetItemPlacementInquiry(ItemPlacementProposition proposition)
        {
            var position = GetPosition(proposition);
            return new ItemPlacementInquiryResult(GetIsPossible(proposition), position.x, position.y, GetReplacement(proposition));

            IItem GetReplacement(ItemPlacementProposition itemPlacementProposition)
            {
                throw new NotImplementedException();
            }

            Vector2Int GetPosition(ItemPlacementProposition proposition)
            {
                throw new NotImplementedException();
            }

            bool GetIsPossible(ItemPlacementProposition proposition)
            {
                throw new NotImplementedException();
            }
        }
        
        private void PlaceItem(IItem item, Vector2Int coords)
        {
            _gridSpace.Insert(new GridSpaceItem(item.Id, item.Size, coords));
            _items[item.Id] = item;
        }
    }
}