using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using Libs.GridSpace;
using Libs.Utils.DotNet;
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

        public override bool ContainsItem(IItem item) => 
            _items.ContainsKey(item.Id);

        protected override void PlaceItemInternal(IItem item, Vector2 posNormalized)
        {
            _gridSpace.Insert(new GridSpaceItem(item.Id, item.Size, NormalizedToGridPosition(posNormalized)));
            _items[item.Id] = item;
        }

        private Vector2 GetItemPositionNormalized(string id)
        {
            var item = _gridSpace.GetItem(id);
            return GridPositionToNormalized(item.Size, item.Position);
        }

        private Vector2 GridPositionToNormalized(Vector2Int itemSize, Vector2Int gridPosition) => 
            (Vector2)gridPosition / _gridSpace.Size + (Vector2)itemSize / 2 / _gridSpace.Size;

        private Vector2Int ItemNormalizedToGridPosition(Vector2 normalizedPosition, Vector2Int itemSize)
        {
            var pos = normalizedPosition * _gridSpace.Size - itemSize / 2;
            return new Vector2Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
        }

        private Vector2Int NormalizedToGridPosition(Vector2 normalizedPosition) => 
            new(Mathf.RoundToInt(normalizedPosition.x * _gridSpace.Size.x), Mathf.RoundToInt(normalizedPosition.y * _gridSpace.Size.y));

        protected override void RemoveItemInternal(IItem item)
        {
            if (!_gridSpace.Remove(item.Id) || !_items.Remove(item.Id))
                throw new InvalidOperationException();
        }

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, Vector2 posNormalized)
        {
            var gridPos = ItemNormalizedToGridPosition(posNormalized, item.Size);
            var curatedNormPos = ItemGridToNormalizedPosition(gridPos, item.Size);
            var isPossible = _gridSpace.IntersectsWithOneOrLessItems(new GridSpaceItem(item.Id, item.Size,gridPos), out var intersection);
            return new ItemPlacementProspect(isPossible, curatedNormPos, intersection.IsNullOrEmpty() ? null : _items[intersection]);
        }

        private Vector2 ItemGridToNormalizedPosition(Vector2Int itemPos, Vector2Int itemSize)
        {
            var itemCenterPos = itemPos + (Vector2)itemSize / 2 - Vector2.one * .5f;
            return itemCenterPos / _gridSpace.Size;
        }
    }
}