namespace Common.MoveSets
{
    public interface ITryTransitionToMoveGetter
    {
        bool TryTransitionTo(IMove move);
    }
}