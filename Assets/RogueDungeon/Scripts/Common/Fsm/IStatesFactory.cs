namespace Common.Fsm
{
    public interface IStatesFactory
    {
        T Create<T>() where T : IState;
    }
}