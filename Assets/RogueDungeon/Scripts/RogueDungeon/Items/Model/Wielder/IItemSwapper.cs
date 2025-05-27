namespace RogueDungeon.Items.Model
{
    public interface IItemSwapper
    {
        IHandheldItem CurrentItem { get; set; }
        IHandheldItem IntendedItem { get; set; }        
    }
}