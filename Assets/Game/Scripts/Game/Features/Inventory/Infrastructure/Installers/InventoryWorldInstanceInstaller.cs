using System;
using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Infrastructure.View;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Factories
{
    public class InventoryWorldInstanceInstaller : MonoInstaller
    {
        [Serializable]
        private struct ContainerIdPair
        {
            [HorizontalGroup] public ContainerId Id;
            [FormerlySerializedAs("Container")] [HorizontalGroup] public ContainerView containerView;
        }

        [SerializeField] private View.InventoryView _view;
        [SerializeField] private ContainerIdPair[] _containerViews;
        [SerializeField] private DraggableArea _draggableArea;
        
        private Domain.Inventory _inventory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<View.InventoryView>().FromInstance(_view).AsSingle();
            
            foreach (var pair in _containerViews) 
                Container.BindInterfacesAndSelfTo<ContainerPresenter>().FromSubContainerResolve().ByMethod(subcontainer => CreateContainer(pair, subcontainer))
                    .AsCached().NonLazy();

            Container.BindInterfacesTo<DraggableArea>().FromInstance(_draggableArea).AsSingle();
            Container.BindInterfacesTo<GraphicRaycaster>().AsSingle();
            Container.BindInterfacesAndSelfTo<Mediator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<DragItemState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScanForItemState>().AsSingle();
        }

        private void CreateContainer(ContainerIdPair pair, DiContainer subcontainer)
        {
            subcontainer.BindInterfacesTo<ContainerView>().FromInstance(pair.containerView).AsSingle();
            subcontainer.Bind<ItemContainer>().FromMethod(() => (_inventory ??= subcontainer.Resolve<Domain.Inventory>()).GetContainer(pair.Id)).AsSingle();
            subcontainer.BindInterfacesAndSelfTo<ContainerPresenter>().AsSingle();
        }
    }
}