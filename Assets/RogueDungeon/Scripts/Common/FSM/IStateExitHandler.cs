namespace Common.FSM
{
    public interface IStateExitHandler : IStateHandler
    {
        void OnExit();
    }
}