using System;
using System.Collections.Generic;
using Game.Libs.Items;
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

        public void PlaceItem(IItem item, Vector2 posNormalized)
        {
            PlaceItemInternal(item, posNormalized);
            OnContentChanged?.Invoke();
        }

        public void RemoveItem(IItem item)
        {
            RemoveItemInternal(item);
            OnContentChanged?.Invoke();
        }

        public abstract bool ContainsItem(IItem item);
        public abstract ItemPlacementProspect GetItemPlacementProspect(IItem item, Vector2 posNormalized);
        protected abstract void RemoveItemInternal(IItem item);
        protected abstract void PlaceItemInternal(IItem item, Vector2 posNormalized);
    }
}