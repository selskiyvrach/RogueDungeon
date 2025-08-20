using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryWorldScreenFactory : IFactory<Transform, GameObject>
    {
        private readonly InventoryWorldInstanceInstaller _worldInstancePrefab;
        private readonly DiContainer _container;

        public InventoryWorldScreenFactory(DiContainer container, InventoryWorldInstanceInstaller worldInstancePrefab)
        {
            _container = container;
            _worldInstancePrefab = worldInstancePrefab;
        }

        public GameObject Create(Transform parent) => 
            _container.InstantiatePrefab(_worldInstancePrefab, parent);
    }
}