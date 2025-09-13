namespace Game.Libs.Items
{
    public class Armor : Item, ISlotableItem
    {
        public SlotCategory SlotCategory => SlotCategory.Armor;

        public Armor(IItemConfig itemConfig, string id) : base(itemConfig, id)
        {
        }
    }
}