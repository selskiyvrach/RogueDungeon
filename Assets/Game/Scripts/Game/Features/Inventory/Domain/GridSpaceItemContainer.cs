using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using Libs.GridSpace;
using Libs.Utils.DotNet;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Features.Inventory.Domain
{
    public class GridSpaceItemContainer : ItemContainer
    {
        private readonly GridSpace _gridSpace;
        private readonly Dictionary<string, IItem> _items = new();

        public GridSpaceItemContainer(int columns, int rows, ContainerId id) : base(id) => 
            _gridSpace = new GridSpace(columns, rows);

        public override IEnumerable<(IItem item, PositionNormalized position)> GetItems() => 
            _items.Select(n => (n.Value, GetItemPositionNormalized(n.Value)));

        public override bool ContainsItem(IItem item) => 
            _items.ContainsKey(item.Id);

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, PositionNormalized pos)
        {
           NormalizedPosToCornersGridPos(pos, item.Size, out var corner1, out var corner3);

            // move an item inside the container if one of its corners sticks out
            var offsetX = Mathf.Min(corner1.x, 0) + Mathf.Max(corner3.x - (_gridSpace.Size.x - 1), 0);
            var offsetY = Mathf.Min(corner1.y, 0) + Mathf.Max(corner3.y - (_gridSpace.Size.y - 1), 0);
            corner1 -= new Vector2Int(offsetX, offsetY);
            
            var position = GetItemPositionNormalized(new GridSpaceItem(item.Id, item.Size, corner1));
            
            var isPossible = _gridSpace.IntersectsWithOneOrLessItems(new GridSpaceItem(item.Id, item.Size, corner1), out var intersection);
            return new ItemPlacementProspect(isPossible, position, intersection.IsNullOrEmpty() ? null : _items[intersection]);
        }

        protected override void PlaceItemInternal(IItem item, PositionNormalized position)
        {
            NormalizedPosToCornersGridPos(position, item.Size, out var corner1, out var corner3);
            _gridSpace.Insert(new GridSpaceItem(item.Id, item.Size, corner1));
            _items[item.Id] = item;
        }

        protected override void RemoveItemInternal(IItem item)
        {
            if (!_gridSpace.Remove(item.Id) || !_items.Remove(item.Id))
                throw new InvalidOperationException();
        }

        private void NormalizedPosToCornersGridPos(PositionNormalized pos, Vector2Int itemSize, out Vector2Int corner1, out Vector2Int corner3)
        {
            var gridCenterPos = Vector2Int.FloorToInt(pos.ToVector2() * _gridSpace.Size);
            corner1 = Vector2Int.FloorToInt(gridCenterPos - (Vector2)itemSize / 2 + Vector2.one / 2);
            corner3 = Vector2Int.FloorToInt(gridCenterPos + (Vector2)itemSize / 2 - Vector2.one / 2);
        }

        private PositionNormalized GetItemPositionNormalized(GridSpaceItem item)
        {
            var centerOfItemPos = Vector2.Lerp((Vector2)item.Position / _gridSpace.Size, (Vector2)(item.Position + item.Size - Vector2Int.one) / _gridSpace.Size, 0.5f);
            return PositionNormalized.FromVector2(centerOfItemPos + Vector2.one / 2 / _gridSpace.Size);
        }

        private Vector2Int NormalizedToGridPosition(PositionNormalized position) => 
            new(Mathf.RoundToInt(position.X * _gridSpace.Size.x), Mathf.RoundToInt(position.Y * _gridSpace.Size.y));

        private PositionNormalized GetItemPositionNormalized(IItem item) => 
            GetItemPositionNormalized(new GridSpaceItem(item.Id, item.Size, _gridSpace.GetItem(item.Id).Position));
    }
}