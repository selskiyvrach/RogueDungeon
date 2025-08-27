using System;
using System.Collections.Generic;
using Game.Libs.Items;
using Libs.Commands;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public abstract class ItemContainer
    {
        public event Action OnContentChanged;
        public ContainerId Id { get; }
        protected ItemContainer(ContainerId id) => 
            Id = id;

        public abstract IEnumerable<(IItem item, Vector2 posNormalized)> GetItems();

        public void PlaceItem(ItemPlacement placement)
        {
            PlaceItemInternal(placement);
            OnContentChanged?.Invoke();
        }

        public void PlaceItem(IItem item, ItemPlacementInquiryResult placementInquiry) => 
            PlaceItem(GetItemPlacementFromProjection(item, placementInquiry));

        public IItem ExtractItem(string itemId)
        {
            var item = ExtractItemInternal(itemId);
            OnContentChanged?.Invoke();
            return item;
        }

        public abstract ItemPlacementInquiryResult GetItemPlacementInquiry(ItemPlacementProposition proposition);
        protected abstract IItem ExtractItemInternal(string itemId);
        protected abstract void PlaceItemInternal(ItemPlacement placement);
        protected abstract ItemPlacement GetItemPlacementFromProjection(IItem item, ItemPlacementInquiryResult placementInquiry);
    }
}