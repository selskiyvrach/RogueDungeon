namespace Common.Fsm
{
    public interface IIdBasedStatesProvider
    {
        IState Get(string id);
    }
}