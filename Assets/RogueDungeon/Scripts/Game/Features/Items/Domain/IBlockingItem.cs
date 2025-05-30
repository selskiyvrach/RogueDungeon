namespace Game.Features.Items.Domain
{
    public interface IBlockingItem : IHandheldItem
    {
        public BlockingTier BlockingTier { get; }
        public float BlockStaminaCostMultiplier { get; }
    }
}