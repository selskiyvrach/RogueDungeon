using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class FreeSpaceItemContainer : ItemContainer
    {
        private readonly List<(IItem item, PositionNormalized position)> _items = new();

        public FreeSpaceItemContainer(ContainerId id) : base(id)
        {
        }

        public override IEnumerable<(IItem item, PositionNormalized position)> GetItems() => 
            _items;

        public override bool ContainsItem(IItem item) => 
            _items.Any(n => n.item == item);

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, PositionNormalized pos) =>
            new(IsPossible: true, pos, ReplacedItem: null);

        protected override void PlaceItemInternal(IItem item, PositionNormalized position) => 
            _items.Add((item, position));

        protected override void RemoveItemInternal(IItem item)
        {
            var itemToRemove = _items.First(n => n.item == item);
            if (!_items.Remove(itemToRemove))
                throw new InvalidOperationException();
        }
    }
}