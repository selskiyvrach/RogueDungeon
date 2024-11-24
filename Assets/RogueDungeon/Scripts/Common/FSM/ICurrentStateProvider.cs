namespace Common.FSM
{
    public interface ICurrentStateProvider
    {
        IState CurrentState { get; }
    }
}