namespace Common.MoveSets
{
    internal interface IPendingMoveGetter
    {
        IMove PendingMove { get; }
    }
}