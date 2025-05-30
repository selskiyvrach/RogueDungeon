using Game.Features.Inventory.Shared;
using Game.Features.Inventory.View;
using Game.Features.Items.Domain;
using UnityEngine;

namespace Game.Features.Inventory.Presenter
{
    public class ItemPresenter 
    {
        private readonly IItem _item;
        private readonly InventoryItemView _view;

        public int InstanceId => _item.InstanceId;
        public Vector2Int Size => _item.Config.Size;
        public InventoryItemView Prefab { get; }

        public bool IsEquippableIntoSlotType(SlotType slotType)
        {
            throw new System.NotImplementedException();
        }

        public ItemPresenter(IItem item ) => 
            _item = item;
    }
}