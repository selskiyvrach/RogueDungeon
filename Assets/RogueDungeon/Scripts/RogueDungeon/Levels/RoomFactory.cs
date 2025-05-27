using Common.UtilsDotNet;
using UnityEngine;
using Zenject;

namespace Levels
{
    public class RoomFactory : IFactory<RoomConfig, Room>
    {
        private readonly DiContainer _container;
        private readonly Transform _parent;

        public RoomFactory(DiContainer container, Transform parent)
        {
            _container = container;
            _parent = parent;
        }

        public Room Create(RoomConfig param1)
        {
            var presenter = _container.InstantiatePrefab(param1.Prefab, _parent).GetComponent<RoomGameObject>().ThrowIfNull();
            var room = _container.Instantiate<Room>(new object[]{ param1, presenter });
            return room;
        }
    }
}