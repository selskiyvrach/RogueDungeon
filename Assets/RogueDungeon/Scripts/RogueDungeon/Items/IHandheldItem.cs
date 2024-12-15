namespace RogueDungeon.Items
{
    public interface IHandheldItem
    {
        object Id { get; }
        float SheathDuration { get; }
        float UnsheathDuration { get; }
        void SetVisible(bool value);
        void SetEnabled(bool value);
    }
}