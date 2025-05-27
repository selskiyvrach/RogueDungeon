using Inventory.Shared;
using Inventory.View;
using RogueDungeon.Items.Model;
using UnityEngine;

namespace Inventory.Presenter
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