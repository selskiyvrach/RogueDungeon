namespace Libs.Fsm
{
    public interface IStateTransitionStrategy
    {
        IState GetStartState();
        IState GetTransition(IState currentState);
    }
}