using System.Collections;

namespace RogueDungeon.Levels
{
    public abstract class RoomEvent : IRoomEvent
    {
        public abstract RoomEventPriority Priority { get; }

        public virtual IEnumerator ProcessEvent(Room room)
        {
            yield break;
        }
    }
}