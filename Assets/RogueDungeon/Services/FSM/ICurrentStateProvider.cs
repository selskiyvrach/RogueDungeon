namespace RogueDungeon.Services.FSM
{
    public interface ICurrentStateProvider
    {
        IState CurrentState { get; }
    }
}