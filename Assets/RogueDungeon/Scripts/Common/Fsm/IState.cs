namespace Common.Fsm
{
    public interface IState
    {
        void Enter();
        void CheckTransitions(IStateChanger stateChanger);
    }
}