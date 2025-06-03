namespace Game.Features.Items.Domain
{
    public abstract class BlockingItem : HandheldItem, IBlockingItem
    {
        private readonly IBlockingItemConfig _config;

        public abstract BlockingTier BlockingTier { get; }

        public float BlockStaminaCostMultiplier => _config.BlockStaminaCostMultiplier;
        public float BlockImpactAbsorptionAnimationDuration => _config.BlockImpactAbsorptionAnimationDuration;
        public float HoldBlockAnimationDuration => _config.HoldBlockAnimationDuration;
        public float LowerBlockAnimationDuration => _config.LowerBlockAnimationDuration;
        public float RaiseBlockAnimationDuration => _config.RaiseBlockAnimationDuration;

        protected BlockingItem(IBlockingItemConfig config) : base(config) => 
            _config = config;
    }
}