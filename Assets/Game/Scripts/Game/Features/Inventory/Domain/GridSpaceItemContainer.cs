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
            _items.Select(n => (n.Value, GetItemPositionNormalized(n.Key)));

        public override bool ContainsItem(IItem item) => 
            _items.ContainsKey(item.Id);

        protected override void PlaceItemInternal(IItem item, PositionNormalized position)
        {
            _gridSpace.Insert(new GridSpaceItem(item.Id, item.Size, NormalizedToGridPosition(position)));
            _items[item.Id] = item;
        }

        private PositionNormalized GetItemPositionNormalized(string id)
        {
            var item = _gridSpace.GetItem(id);
            return GridPositionToNormalized(item.Size, item.Position);
        }

        private PositionNormalized GridPositionToNormalized(Vector2Int itemSize, Vector2Int gridPosition)
        {
            var vector = (Vector2)gridPosition / _gridSpace.Size + (Vector2)itemSize / 2 / _gridSpace.Size +
                   Vector2.one / 2 / _gridSpace.Size;
            return PositionNormalized.FromVector2(vector);
        }

        private Vector2Int NormalizedToGridPosition(PositionNormalized position) => 
            new(Mathf.RoundToInt(position.X * _gridSpace.Size.x), Mathf.RoundToInt(position.Y * _gridSpace.Size.y));

        protected override void RemoveItemInternal(IItem item)
        {
            if (!_gridSpace.Remove(item.Id) || !_items.Remove(item.Id))
                throw new InvalidOperationException();
        }

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, PositionNormalized pos)
        {
            // find center cell pos
            var gridCenterPos = Vector2Int.FloorToInt(pos.ToVector2() * _gridSpace.Size);
            // find corners
            var corner1 = Vector2Int.FloorToInt(gridCenterPos - (Vector2)item.Size / 2 + Vector2.one / 2);
            var corner3 = Vector2Int.FloorToInt(gridCenterPos + (Vector2)item.Size / 2 - Vector2.one / 2);

            // move an item inside the container if one of its corners sticks out
            var offsetX = Mathf.Min(corner1.x, 0) + Mathf.Max(corner3.x - (_gridSpace.Size.x - 1), 0);
            var offsetY = Mathf.Min(corner1.y, 0) + Mathf.Max(corner3.y - (_gridSpace.Size.y - 1), 0);
            corner1 -= new Vector2Int(offsetX, offsetY);
            corner3 -= new Vector2Int(offsetX, offsetY);
            
            var centerPosNormalized = Vector2.Lerp((Vector2)corner1 / _gridSpace.Size, (Vector2)corner3 / _gridSpace.Size, 0.5f);
            var position = PositionNormalized.FromVector2(centerPosNormalized + Vector2.one / 2 / _gridSpace.Size);
            
            var isPossible = _gridSpace.IntersectsWithOneOrLessItems(new GridSpaceItem(item.Id, item.Size, corner1), out var intersection);
            return new ItemPlacementProspect(isPossible, position, intersection.IsNullOrEmpty() ? null : _items[intersection]);
        }
    }
}