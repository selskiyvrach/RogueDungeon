using Game.Features.Inventory.App.UseCases;
using Game.Features.Inventory.Infrastructure.Factories;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class InventoryInstanceInstaller : MonoInstaller
    {
        [SerializeField] private Transform _inventoryViewParent;
        [SerializeField] private InventoryWorldInstanceInstaller _inventoryViewPrefab;

        
        public override void InstallBindings()
        {
            Container.Bind<Domain.Inventory>().AsSingle();
            Container.Bind<IFactory<Transform, InventoryView>>().To<InventoryWorldScreenFactory>().AsSingle().WithArguments(new object[]{_inventoryViewPrefab} );
            Container.BindInterfacesTo<InventoryScreenSpawner>().AsSingle().WithArguments(new object[]{ _inventoryViewParent});
            
            Container.BindInterfacesTo<ShowHideWorldInventoryUseCase>().AsSingle().NonLazy();
        }
    }
}