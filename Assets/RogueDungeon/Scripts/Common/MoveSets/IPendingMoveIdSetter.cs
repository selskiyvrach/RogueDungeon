namespace Common.MoveSets
{
    internal interface IPendingMoveSetter
    {
        IMove PendingMove { set; }
    }
}