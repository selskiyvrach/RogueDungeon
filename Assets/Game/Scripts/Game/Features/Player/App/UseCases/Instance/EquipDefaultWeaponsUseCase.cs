using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using Game.Libs.Items.Factory;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class EquipDefaultWeaponsUseCase
    {
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly IItemFactory _itemFactory;

        public EquipDefaultWeaponsUseCase(Inventory.Domain.Inventory inventory, IItemFactory itemFactory)
        {
            _inventory = inventory;
            _itemFactory = itemFactory;
            _inventory.GetContainer(ContainerId.LeftHand0).PlaceItem(_itemFactory.Create(ItemIds.AXE), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.RightHand0).PlaceItem(_itemFactory.Create(ItemIds.SHIELD), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.RightHand1).PlaceItem(_itemFactory.Create(ItemIds.PICKAXE), PositionNormalized.Center);
            _inventory.GetContainer(ContainerId.LeftHand1).PlaceItem(_itemFactory.Create(ItemIds.SHIELD), PositionNormalized.Center);
            
            _inventory.GetContainer(ContainerId.Armor0).PlaceItem(_itemFactory.Create(ItemIds.ARMOR), PositionNormalized.Center);
            
            _inventory.GetContainer(ContainerId.Consumable2).PlaceItem(_itemFactory.Create(ItemIds.DRUMSTICK), PositionNormalized.Center);
            
            _inventory.GetContainer(ContainerId.Backpack0).PlaceItem(_itemFactory.Create(ItemIds.AXE), new PositionNormalized(.5f, .3f));
        }
    }
}