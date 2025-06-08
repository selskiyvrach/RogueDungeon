using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Infrastructure.View;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [SerializeField] private InventoryView _inventoryView;

        public override void InstallBindings()
        {
            Container.Bind<Domain.Inventory>().AsSingle().WithArguments(new object[] {new InventoryConfig()});
        }
    }
}