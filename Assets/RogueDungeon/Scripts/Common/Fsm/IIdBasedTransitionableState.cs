namespace Common.Fsm
{
    public interface IIdBasedTransitionableState : IState
    {
        string Id { get; }
        string GetTransitionStateId();
    }
}