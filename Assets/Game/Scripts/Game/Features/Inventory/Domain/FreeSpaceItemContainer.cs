using System;
using System.Collections.Generic;
using System.Linq;
using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public class FreeSpaceItemContainer : ItemContainer
    {
        private readonly List<(IItem item, Vector2 position)> _items = new();

        public FreeSpaceItemContainer(ContainerId id) : base(id)
        {
        }

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems() => 
            _items;

        public override bool ContainsItem(IItem item) => 
            _items.Any(n => n.item == item);

        public override ItemPlacementProspect GetItemPlacementProspect(IItem item, Vector2 posNormalized) =>
            new(IsPossible: true, posNormalized, ReplacedItem: null);

        protected override void PlaceItemInternal(IItem item, Vector2 localPositionNormalized) => 
            _items.Add((item, localPositionNormalized));

        protected override void RemoveItemInternal(IItem item)
        {
            var itemToRemove = _items.First(n => n.item == item);
            if (!_items.Remove(itemToRemove))
                throw new InvalidOperationException();
        }
    }
}