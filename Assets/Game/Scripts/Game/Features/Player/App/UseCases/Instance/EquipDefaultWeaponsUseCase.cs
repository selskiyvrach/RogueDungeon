using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using Game.Libs.Items.Factory;

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
            _inventory.Equip(_itemFactory.Create(ItemIds.AXE), ContainerId.LeftHand0);
            _inventory.Equip(_itemFactory.Create(ItemIds.SHIELD), ContainerId.RightHand0);
            
            _inventory.Equip(_itemFactory.Create(ItemIds.AXE), ContainerId.RightHand1);
            _inventory.Equip(_itemFactory.Create(ItemIds.SHIELD), ContainerId.LeftHand1);
        }
    }
    
}