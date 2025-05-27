namespace RogueDungeon.Items.Model
{
    public interface IHandheldItem : IItem
    {
        float UnsheathDuration { get; }
    }
}