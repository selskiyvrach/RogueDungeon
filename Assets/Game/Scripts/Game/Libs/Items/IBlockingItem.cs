namespace Game.Libs.Items
{
    public interface IBlockingItem : IHandheldItem
    {
        public BlockingTier BlockingTier { get; }
        public float BlockStaminaCostMultiplier { get; }
        float BlockImpactAbsorptionAnimationDuration { get; }
        float HoldBlockAnimationDuration { get; }
        float LowerBlockAnimationDuration { get; }
        float RaiseBlockAnimationDuration { get; }
    }
}