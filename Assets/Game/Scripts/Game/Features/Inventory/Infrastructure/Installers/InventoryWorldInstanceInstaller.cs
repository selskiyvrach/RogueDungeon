using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryWorldInstanceInstaller : MonoInstaller
    {
        [SerializeField] private InventoryView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<IInventoryView>().FromInstance(_view).AsSingle();
            Container.Bind<InventoryPresenter>().FromNew().AsSingle();
        }
    }
}