namespace Game.Libs.Items
{
    public interface IHandheldItemConfig : IItemConfig
    {
        float IdleAnimationDuration { get; }
        float UnsheathDuration { get; }
    }
}