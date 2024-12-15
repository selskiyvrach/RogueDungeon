using RogueDungeon.Items;

namespace RogueDungeon.Behaviours.HandheldEquipmentBehaviour
{
    public interface ICurrentEquipmentState
    {
        public IHandheldItem CurrentItem { get; set; }
        public IHandheldItem IntendedItem { get; set; }
    }
}