namespace RogueDungeon.Items.Model
{
    public interface IItemTransitionsLockedProvider
    {
        bool ItemTransitionsAreLocked { get; }
    }
}