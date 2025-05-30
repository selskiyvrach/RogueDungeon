namespace Libs.Fsm
{
    public interface ITypeBasedStateChanger
    {
        void ChangeState<T>() where T : class, IState;
    }
}