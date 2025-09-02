using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class LootAreaInstanceInstaller : MonoInstaller
    {
        [SerializeField] private ContainerView _lootArea;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ContainerView>().FromInstance(_lootArea).AsCached();
            Container.BindInterfacesTo<ContainerPresenter>().AsCached();
            Container.BindInterfacesAndSelfTo<ItemContainer>().FromMethod(
                _ => Container.Resolve<ILootManager>().GetRoomLootContainer(Container.Resolve<Vector2Int>())).AsCached();
        }
    }
}