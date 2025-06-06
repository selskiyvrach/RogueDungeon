namespace Game.Features.Player.Domain.Movesets.Items.Interfaces
{
    public interface IItemTransitionsLockedProvider
    {
        bool ItemTransitionsAreLocked { get; }
    }
}