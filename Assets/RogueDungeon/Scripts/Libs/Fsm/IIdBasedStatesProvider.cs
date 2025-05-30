namespace Libs.Fsm
{
    public interface IIdBasedStatesProvider
    {
        IState Get(string id);
    }
}