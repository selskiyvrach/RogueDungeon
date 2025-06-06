using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Infrastructure.View;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryView _inventoryView;

        public override void InstallBindings()
        {
            Container.NewSingleInterfacesAndSelf<InventoryPresenter>();
            Container.NewSingle<Inventory.Domain.Inventory>();
            Container.InstanceSingle(_inventoryView);
        }
    }
}