namespace Game.Features.Items.Domain
{
    public interface IHandheldItem : IItem
    {
        float UnsheathDuration { get; }
    }
}