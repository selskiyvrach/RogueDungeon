namespace Game.Libs.Items
{
    public class Consumable : Item, ISlotableItem
    {
        public SlotCategory SlotCategory => SlotCategory.Consumable;

        public Consumable(IItemConfig itemConfig, string id) : base(itemConfig, id)
        {
        }
    }
}