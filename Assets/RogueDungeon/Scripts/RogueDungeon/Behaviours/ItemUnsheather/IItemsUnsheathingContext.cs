using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public interface IChangingHandheldItemsInfo
    {
        public IHandheldItem CurrentItem { get; set; }
        public IHandheldItem IntendedItem { get; set; }
    }
}