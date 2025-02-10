namespace Common.Fsm
{
    public interface ITypeBasedStatesProvider 
    {
        TConcrete Get<TConcrete>() where TConcrete : class, IState;
    }
    
    public interface IIdBasedStatesProvider 
    {
        IState Get(string id);
    }
}