namespace RogueDungeon.Items
{
    public interface IHandheldItem
    {
        IItem Item { get; }
        void SetVisible(bool value);
        void SetEnabled(bool value);
    }
}