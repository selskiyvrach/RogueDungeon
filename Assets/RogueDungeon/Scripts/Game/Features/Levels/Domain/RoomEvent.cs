using UnityEngine.Assertions;

namespace Game.Features.Levels.Domain
{
    public abstract class RoomEvent : IRoomEvent
    {
        public abstract RoomEventPriority Priority { get; }
        public Room Room { get; set; }
        public abstract bool IsFinished { get; }

        public virtual void Tick(float timeDelta)
        {
        }

        public virtual void Enter() => 
            Assert.IsNotNull(Room);

        public virtual void Exit() => 
            Room = null;
    }
}