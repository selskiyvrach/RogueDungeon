using Libs.Fsm;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Levels.Domain
{
    public interface IRoom : IEnterableState, IExitableState, ITickable
    {
        Vector2Int Coordinates { get; }
        AdjacentRooms AdjacentRooms { get; }
        bool CanLeave { get; }
        void AddEvent(IRoomEvent roomEvent);
    }
}