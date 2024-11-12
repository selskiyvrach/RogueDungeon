namespace RogueDungeon.Services.FSM
{
    public interface ITransition
    {
        bool CanTransit(StatesContainer statesContainer, out IState transitionTo);
    }
}