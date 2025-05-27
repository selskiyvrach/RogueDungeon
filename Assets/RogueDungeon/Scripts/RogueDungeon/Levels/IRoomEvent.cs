using Common.Fsm;
using Common.Lifecycle;

namespace Levels
{
    public interface IRoomEvent : ITickable, IEnterableState, IExitableState, IFinishable
    {
        RoomEventPriority Priority { get; }
        Room Room { set; }
    }
}