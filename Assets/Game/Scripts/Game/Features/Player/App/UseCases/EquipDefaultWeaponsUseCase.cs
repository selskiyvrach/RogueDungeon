using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using Game.Libs.Items.Factory;

namespace Game.Features.Player.App.UseCases
{
    public class EquipDefaultWeaponsUseCase
    {
        private readonly Inventory.Domain.Inventory _inventory;
        private readonly ItemFactory _itemFactory;

        public EquipDefaultWeaponsUseCase(Inventory.Domain.Inventory inventory, ItemFactory itemFactory)
        {
            _inventory = inventory;
            _itemFactory = itemFactory;
            _inventory.Equip(_itemFactory.Create("axe"), SlotId.HANDHELD_L_0);
            _inventory.Equip(_itemFactory.Create("shield"), SlotId.HANDHELD_R_0);
        }
    }
}