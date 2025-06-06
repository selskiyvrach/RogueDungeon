namespace Game.Libs.Items
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