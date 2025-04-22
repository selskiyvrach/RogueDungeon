using Zenject;

namespace RogueDungeon.Levels
{
    public class RoomEventFactory : IFactory<RoomEventConfig, IRoomEvent>
    {
        private readonly DiContainer _container;

        public RoomEventFactory(DiContainer container) => 
            _container = container;

        public IRoomEvent Create(RoomEventConfig param1) => 
            (IRoomEvent)_container.Instantiate(param1.EventType, new []{param1});
    }
}