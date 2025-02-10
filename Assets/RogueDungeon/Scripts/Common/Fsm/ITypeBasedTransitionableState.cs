namespace Common.Fsm
{
    public interface ITypeBasedTransitionableState : IState
    {
        void CheckTransitions(ITypeBasedStateChanger stateChanger);
    }
}