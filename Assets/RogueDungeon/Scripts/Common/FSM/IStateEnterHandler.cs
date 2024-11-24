namespace Common.FSM
{
    public interface IStateEnterHandler : IStateHandler
    {
        void OnEnter();
    }
}