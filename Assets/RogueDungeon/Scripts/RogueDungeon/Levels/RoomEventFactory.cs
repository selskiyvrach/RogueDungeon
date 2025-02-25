using Zenject;

namespace RogueDungeon.Levels
{
    public class RoomEventFactory : IFactory<RoomEventConfig, IRoomEvent>
    {
        private readonly DiContainer _container;

        public RoomEventFactory(DiContainer container) => 
            _container = container;

        public IRoomEvent Create(RoomEventConfig config) => 
            (IRoomEvent)_container.Instantiate(config.EventType, new []{config});
    }
}