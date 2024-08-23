namespace RogueDungeon.StateMachine
{
    public interface ITransition
    {
        bool CanTransit(StatesContainer statesContainer, out IState transitionTo);
    }
}