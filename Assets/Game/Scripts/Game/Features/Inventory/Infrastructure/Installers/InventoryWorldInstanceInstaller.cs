using System;
using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Infrastructure.View;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryWorldInstanceInstaller : MonoInstaller
    {
        [Serializable]
        private struct ContainerIdPair
        {
            [HorizontalGroup] public ContainerId Id;
            [HorizontalGroup] public Container Container;
        }

        [SerializeField] private ItemView _itemViewPrefab;
        [SerializeField] private View.Inventory _view;
        [SerializeField] private ContainerIdPair[] _containerViews;
        [SerializeField] private DraggableArea _draggableArea;
        [SerializeField] private Transform _itemsParentTransform;
        
        private Domain.Inventory _inventory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<View.Inventory>().FromInstance(_view).AsSingle();
            
            foreach (var pair in _containerViews) 
                Container.BindInterfacesAndSelfTo<ContainerPresenter>().FromSubContainerResolve().ByMethod(subcontainer => CreateContainer(pair, subcontainer))
                    .AsCached().NonLazy();

            Container.BindInterfacesTo<ItemsViewCachedProvider>().AsSingle();
            Container.BindInterfacesTo<DraggableArea>().FromInstance(_draggableArea).AsSingle();
            Container.BindInterfacesTo<GraphicRaycaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<Mediator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PresentersRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<DragItemState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScanForItemState>().AsSingle();
            Container.BindInterfacesTo<ItemViewFactory>().AsSingle().WithArguments(new object[]{ _itemViewPrefab, _itemsParentTransform });
        }

        private void CreateContainer(ContainerIdPair pair, DiContainer subcontainer)
        {
            subcontainer.BindInterfacesTo<Container>().FromInstance(pair.Container).AsSingle();
            subcontainer.Bind<ItemContainer>().FromMethod(() => (_inventory ??= subcontainer.Resolve<Domain.Inventory>()).GetContainer(pair.Id)).AsSingle();
            subcontainer.BindInterfacesAndSelfTo<ContainerPresenter>().AsSingle();
        }
    }
}