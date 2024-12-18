namespace RogueDungeon.Items.Handling.Unsheather
{
    public interface IChangingHandheldItemsInfo
    {
        public IHandheldItem CurrentItem { get; set; }
        public IHandheldItem IntendedItem { get; set; }
    }
}