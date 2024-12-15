namespace RogueDungeon.Behaviours.EquipmentBehaviour
{
    public interface ICurrentEquipmentState
    {
        public IItemHandle CurrentItem { get; set; }
        public IItemHandle IntendedItem { get; set; }
    }
}