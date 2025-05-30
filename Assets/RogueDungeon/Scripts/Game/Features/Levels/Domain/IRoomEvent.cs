using Libs.Fsm;
using Libs.Lifecycle;

namespace Game.Features.Levels.Domain
{
    public interface IRoomEvent : ITickable, IEnterableState, IExitableState, IFinishable
    {
        RoomEventPriority Priority { get; }
        Room Room { set; }
    }
}