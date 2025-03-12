using Common.Lifecycle;

namespace Common.Fsm
{
    public interface IState
    {
    }

    public interface IPriorityState
    {
        int Priority { get; }
    }

}