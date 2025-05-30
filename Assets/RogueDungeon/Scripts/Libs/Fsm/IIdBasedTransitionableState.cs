namespace Libs.Fsm
{
    public interface IIdBasedTransitionableState : IState
    {
        string Id { get; }
        string GetTransitionStateId();
    }
}