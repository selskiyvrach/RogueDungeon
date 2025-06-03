namespace Libs.Fsm
{
    public interface ITypeBasedStatesProvider 
    {
        TConcrete Get<TConcrete>() where TConcrete : class, IState;
    }
}