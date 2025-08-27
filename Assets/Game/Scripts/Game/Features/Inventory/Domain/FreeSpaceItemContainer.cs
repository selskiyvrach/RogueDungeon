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

        private void PlaceItem(IItem item, Vector2 localPositionNormalized) => 
            _items.Add((item, localPositionNormalized));

        public override IEnumerable<(IItem item, Vector2 posNormalized)> GetItems() => 
            _items;

        protected override void PlaceItemInternal(ItemPlacement placement) => 
            PlaceItem(placement.Item, ((FreeSpaceItemPlacement)placement).Position);

        protected override ItemPlacement GetItemPlacementFromProjection(IItem item, ItemPlacementInquiryResult placementInquiry) => 
            new FreeSpaceItemPlacement(item, new Vector2(placementInquiry.XNormalized, placementInquiry.YNormalized));

        protected override IItem ExtractItemInternal(string itemId)
        {
            var item = _items.First(n => n.item.Id == itemId);
            _items.Remove(item);
            return item.item;
        }

        public override ItemPlacementInquiryResult GetItemPlacementInquiry(ItemPlacementProposition proposition) => 
            new(IsPossible: true, proposition.XNormalized, proposition.YNormalized, ReplacedItem: null);
    }
}