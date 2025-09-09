using Game.Features.Inventory.Infrastructure.Installers;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryWorldScreenFactory : IFactory<Transform, InventoryView>
    {
        private readonly InventoryWorldInstanceInstaller _worldInstancePrefab;
        private readonly DiContainer _container;

        public InventoryWorldScreenFactory(DiContainer container, InventoryWorldInstanceInstaller worldInstancePrefab)
        {
            _container = container;
            _worldInstancePrefab = worldInstancePrefab;
        }

        public InventoryView Create(Transform parent)
        {
            var result = _container.InstantiatePrefabForComponent<InventoryView>(_worldInstancePrefab, parent);
            result.transform.localPosition = Vector3.zero;
            return result;
        }
    }
}