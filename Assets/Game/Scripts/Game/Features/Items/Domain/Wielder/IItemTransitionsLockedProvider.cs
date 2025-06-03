namespace Game.Features.Items.Domain.Wielder
{
    public interface IItemTransitionsLockedProvider
    {
        bool ItemTransitionsAreLocked { get; }
    }
}