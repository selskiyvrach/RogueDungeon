namespace RogueDungeon.Items
{
    public interface IHandheldItem
    {
        IItem Item { get; }
        void SetVisible(bool value);
        void SetEnabled(bool value);
        float SheathDuration { get; }
        float UnsheathDuration { get; }
    }

    public interface IItemManipulationDurationCalculator
    {
        float Calculate(IItem item);
    }
}