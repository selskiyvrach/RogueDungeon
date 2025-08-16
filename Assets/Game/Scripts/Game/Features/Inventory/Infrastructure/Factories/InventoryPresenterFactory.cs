using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryPresenterFactory : IFactory<Transform, InventoryPresenter>
    {
        private readonly InventoryWorldInstanceInstaller _worldInstancePrefab;
        private readonly DiContainer _container;

        public InventoryPresenterFactory(DiContainer container, InventoryWorldInstanceInstaller worldInstancePrefab)
        {
            _container = container;
            _worldInstancePrefab = worldInstancePrefab;
        }

        public InventoryPresenter Create(Transform parent) => 
            _container
                .InstantiatePrefab(_worldInstancePrefab, parent)
                .GetComponent<Context>()
                .Container
                .Resolve<InventoryPresenter>();
    }
}