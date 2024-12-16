namespace Common.Fsm
{
    public interface IState
    {
        void CheckTransitions(IStateChanger stateChanger);
    }
}