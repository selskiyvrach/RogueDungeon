using RogueDungeon.Items;
using RogueDungeon.Player.Model.Inventory;
using UnityEngine;

namespace Player.ViewModel
{
    public class ItemViewModel : IItemViewModel
    {
        private readonly IItem _item;

        public int InstanceId => _item.InstanceId;
        public Vector2Int Size => _item.Config.Size;
        public Sprite Sprite => _item.Config.Sprite;
        public bool IsEquippableIntoSlotType(SlotType slotType)
        {
            throw new System.NotImplementedException();
        }

        public ItemViewModel(IItem item) => 
            _item = item;
    }
}