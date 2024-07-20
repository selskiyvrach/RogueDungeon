namespace RogueDungeon.StateMachine
{
    public interface IStateExitHandler : IStateHandler
    {
        void OnExit();
    }
}