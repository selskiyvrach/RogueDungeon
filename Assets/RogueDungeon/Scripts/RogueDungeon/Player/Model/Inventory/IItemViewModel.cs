using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public interface IItemViewModel
    {
        public int InstanceId { get; }
        public Vector2Int Size { get; }
        public Sprite Sprite { get; }
        public bool IsEquippableIntoSlotType(SlotType slotType);
    }
}