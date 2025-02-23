using System.Collections;

namespace RogueDungeon.Levels
{
    public interface IRoomEvent
    {
        RoomEventPriority Priority { get; }
        IEnumerator ProcessEvent(Room room);
    }
}