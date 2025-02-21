namespace RogueDungeon.Levels
{
    public interface IRoomEvent
    {
        RoomEventPriority Priority { get; }
        void Trigger(Room room);
    }
}