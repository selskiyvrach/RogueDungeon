namespace Common.Fsm
{
    public interface IStateChanger
    {
        void To<T>() where T : IState;
    }
}