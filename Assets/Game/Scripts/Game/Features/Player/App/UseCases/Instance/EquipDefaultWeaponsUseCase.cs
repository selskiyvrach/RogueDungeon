using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using Game.Libs.Items.Factory;
using UnityEngine;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class EquipDefaultWeaponsUseCase
    {
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly ItemFactory _itemFactory;

        public EquipDefaultWeaponsUseCase(Inventory.Domain.Inventory inventory, ItemFactory itemFactory)
        {
            _inventory = inventory;
            _itemFactory = itemFactory;
            _inventory.GetContainer(ContainerId.LeftHand0).PlaceItem(_itemFactory.Create(ItemIds.AXE), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.RightHand0).PlaceItem(_itemFactory.Create(ItemIds.SHIELD), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.RightHand1).PlaceItem(_itemFactory.Create(ItemIds.AXE), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.LeftHand1).PlaceItem(_itemFactory.Create(ItemIds.SHIELD), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.Backpack0).PlaceItem(_itemFactory.Create(ItemIds.AXE), PositionNormalized.Zero);
        }
    }
}