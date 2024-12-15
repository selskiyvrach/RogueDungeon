namespace Common.Fsm
{
    public interface IStatesFactory 
    {
        TConcrete Create<TConcrete>() where TConcrete : class, IState;
    }
}