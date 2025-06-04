using Game.Features.Levels.Domain;
using Game.Features.Levels.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Factories
{
    public class RoomFactory : IRoomFactory
    {
        private readonly DiContainer _container;
        private readonly Transform _parent;

        public RoomFactory(DiContainer container, Transform parent)
        {
            _container = container;
            _parent = parent;
        }

        public Room Create(IRoomConfig param1)
        {
            var view = _container.InstantiatePrefab(((RoomConfig)param1).Prefab, _parent);
            view.transform.position = new Vector3(param1.Coordinates.x, 0, param1.Coordinates.y);
            
            var model = _container.Instantiate<Room>(new object[]{ param1 });
            return model;
        }
    }
}