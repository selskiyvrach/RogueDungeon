using Common.Fsm;
using Common.Lifecycle;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public interface IRoom : IEnterableState, IExitableState, ITickable
    {
        Vector2Int Coordinates { get; }
        AdjacentRooms AdjacentRooms { get; }
        bool CanLeave { get; }
        void AddEvent(IRoomEvent roomEvent);
    }
}