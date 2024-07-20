namespace RogueDungeon.StateMachine
{
    public interface ICurrentStateProvider
    {
        IState CurrentState { get; }
    }
}