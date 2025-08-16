using UnityEngine;

namespace Game.Libs.Items
{
    public interface IItem
    {
        string Id { get; }
        string TypeId { get; }
        public Vector2Int Size { get; }
    }
    
    public interface ISlotable
    {
        public SlotCategory SlotCategory { get; }
    }

    public interface ISlotableItem : IItem, ISlotable
    {
        
    }
    
    public enum SlotCategory
    {
        None,
        Handheld,
        Armor,
        Helmet,
        Boots,
        Amulet
    }
}