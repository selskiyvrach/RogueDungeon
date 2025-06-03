namespace Game.Features.Items.Domain
{
    public interface IHandheldItemConfig : IItemConfig
    {
        float IdleAnimationDuration { get; }
        float UnsheathDuration { get; }
    }
}