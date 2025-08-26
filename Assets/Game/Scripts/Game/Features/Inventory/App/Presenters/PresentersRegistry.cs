using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class PresentersRegistry : IPresentersRegistry
    {
        private readonly HashSet<ContainerPresenter> _containers = new();
        private readonly HashSet<ItemPresenter> _items = new();
        
        public HashSet<ContainerPresenter> Containers => _containers;
        public HashSet<ItemPresenter> Items => _items;
        
        public void Register(ContainerPresenter container)
        {
            Assert.IsFalse(_containers.Contains(container));
            _containers.Add(container);
        }

        public void Unregister(ContainerPresenter container)
        {
            Assert.IsTrue(_containers.Contains(container));
            _containers.Remove(container);
        }

        public void Register(ItemPresenter item)
        {
            Assert.IsFalse(_items.Contains(item));
            _items.Add(item);
        }

        public void Unregister(ItemPresenter item)
        {
            Assert.IsTrue(_items.Contains(item));
            _items.Remove(item);
        }
    }
}