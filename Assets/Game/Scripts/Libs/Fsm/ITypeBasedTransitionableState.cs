namespace Libs.Fsm
{
    public interface ITypeBasedTransitionableState : IState
    {
        void CheckTransitions(ITypeBasedStateChanger stateChanger);
    }
}