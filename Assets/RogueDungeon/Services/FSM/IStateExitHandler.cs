namespace RogueDungeon.Services.FSM
{
    public interface IStateExitHandler : IStateHandler
    {
        void OnExit();
    }
}