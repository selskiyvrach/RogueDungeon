namespace RogueDungeon.Items.Model
{
    public interface IBlockingItem : IHandheldItem
    {
        public BlockingTier BlockingTier { get; }
        public float BlockStaminaCostMultiplier { get; }
    }
}