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

        [SerializeField] private View.Inventory _view;
        [SerializeField] private ItemProjection _projection;
        [SerializeField] private ContainerIdPair[] _containerViews;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ItemProjection>().FromInstance(_projection).AsSingle();
            
            foreach (var pair in _containerViews)
            {
                var container = Container.CreateSubContainer();
                container.BindInterfacesTo<Container>().FromInstance(pair.Container).AsSingle();
                container.BindInterfacesTo<ContainerPresenter>().AsSingle();
                container.Bind<ItemContainer>().FromMethod(() => container.Resolve<Domain.Inventory>().GetContainer(pair.Id)).AsSingle();
            }
            
            Container.BindInterfacesAndSelfTo<InventoryPresenter>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<Mediator>().AsSingle();
            Container.BindInterfacesAndSelfTo<ElementsRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<DragItemInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<DragItemState>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScanForItemState>().AsSingle();
        }
    }
}