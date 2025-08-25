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
            _inventory.GetContainer(ContainerId.LeftHand0).PlaceItem(new SlotItemPlacement(_itemFactory.Create(ItemIds.AXE)));
            _inventory.GetContainer(ContainerId.RightHand0).PlaceItem(new SlotItemPlacement(_itemFactory.Create(ItemIds.SHIELD)));
            _inventory.GetContainer(ContainerId.RightHand1).PlaceItem(new SlotItemPlacement(_itemFactory.Create(ItemIds.AXE)));
            _inventory.GetContainer(ContainerId.LeftHand1).PlaceItem(new SlotItemPlacement(_itemFactory.Create(ItemIds.SHIELD)));
            _inventory.GetContainer(ContainerId.Backpack0).PlaceItem(new GridSpaceItemPlacement(_itemFactory.Create(ItemIds.AXE), new Vector2Int(8, 1)));
        }
    }
    
}