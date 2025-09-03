using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Infrastructure.Factories;
using Game.Features.Inventory.Infrastructure.View;
using Libs.Utils.DotNet;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class InventoryFeatureInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Canvas _itemsParent;
        [SerializeField] private ItemView _itemViewPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemsViewCachedProvider>().AsSingle();
            Container.BindInterfacesTo<LootManager>().AsSingle();
            Container.BindInterfacesTo<PresentersRegistry>().AsSingle();
            Container.BindInterfacesTo<ItemViewFactory>().AsSingle().WithArguments(new object[]{ _itemViewPrefab });
            Container.BindInterfacesTo<InventoryFeatureInstaller>().FromInstance(this).AsCached().NonLazy();
        }

        public void Initialize() => 
            _itemsParent.worldCamera = Container.Resolve<Camera>().ThrowIfNull();
    }
}