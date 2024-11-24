namespace Common.FSM
{
    public interface IStateTickHandler : IStateHandler
    {
        void OnTick();
    }
}