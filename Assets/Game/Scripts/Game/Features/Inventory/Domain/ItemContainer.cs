using System;
using System.Collections.Generic;
using Game.Libs.Items;

namespace Game.Features.Inventory.Domain
{
    public abstract class ItemContainer
    {
        public event Action OnContentChanged;
        public ContainerId Id { get; }
        protected ItemContainer(ContainerId id) => 
            Id = id;

        public abstract IEnumerable<(IItem item, PositionNormalized position)> GetItems();

        public void PlaceItem(IItem item, PositionNormalized position)
        {
            PlaceItemInternal(item, position);
            OnContentChanged?.Invoke();
        }

        public void RemoveItem(IItem item)
        {
            RemoveItemInternal(item);
            OnContentChanged?.Invoke();
        }

        public abstract bool ContainsItem(IItem item);
        public abstract ItemPlacementProspect GetItemPlacementProspect(IItem item, PositionNormalized pos);
        protected abstract void RemoveItemInternal(IItem item);
        protected abstract void PlaceItemInternal(IItem item, PositionNormalized position);
    }
}