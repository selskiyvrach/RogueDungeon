namespace Common.Fsm
{
    public interface IIdBasedTransitionableState : IState
    {
        string GetTransitionStateId();
    }
}