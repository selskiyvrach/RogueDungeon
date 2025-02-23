using UnityEngine;

namespace RogueDungeon.Levels
{
    public interface IRoom
    {
        Vector2Int Coordinates { get; }
        AdjacentRooms AdjacentRooms { get; }
        void Enter();
        void Exit();
        void AddEvent(IRoomEvent roomEvent);
    }
}