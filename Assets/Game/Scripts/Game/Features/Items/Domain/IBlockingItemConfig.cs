namespace Game.Features.Items.Domain
{
    public interface IBlockingItemConfig : IHandheldItemConfig
    {
        float BlockStaminaCostMultiplier { get; }
        float BlockImpactAbsorptionAnimationDuration { get; }
        float HoldBlockAnimationDuration { get; }
        float LowerBlockAnimationDuration { get; }
        float RaiseBlockAnimationDuration { get; }
    }
}