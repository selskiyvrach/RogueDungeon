namespace Game.Libs.Items
{
    public interface IHandheldItem : IItem
    {
        float UnsheathDuration { get; }
        float IdleAnimationDuration { get; }
    }
}