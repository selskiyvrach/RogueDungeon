namespace Common.FSM
{
    public interface ITransition
    {
        bool CanTransit(StatesContainer statesContainer, out IState transitionTo);
    }
}