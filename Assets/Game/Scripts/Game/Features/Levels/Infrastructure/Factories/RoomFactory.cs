using Game.Features.Levels.Domain;
using Game.Features.Levels.Infrastructure.Configs;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Factories
{
    public class RoomFactory : IFactory<IRoomConfig, RoomAdjacencyInfo, Room>
    {
        private readonly IRoomSpritesConfig _roomSpritesConfig;
        private readonly DiContainer _container;
        private readonly Transform _parent;

        public RoomFactory(DiContainer container, Transform parent, IRoomSpritesConfig roomSpritesConfig)
        {
            _container = container;
            _parent = parent;
            _roomSpritesConfig = roomSpritesConfig;
        }

        public Room Create(IRoomConfig param1, RoomAdjacencyInfo param2)
        {
            _container.Bind<Vector2Int>().FromInstance(param1.Coordinates).AsCached();
            var view = _container.InstantiatePrefab(((RoomConfig)param1).Prefab, _parent);
            view.name = "room_" + param1.Coordinates;
            _container.Unbind<Vector2Int>();
            
            view.transform.position = new Vector3(param1.Coordinates.x, 0, param1.Coordinates.y);
            var sprite = view.GetComponentInChildren<SpriteRenderer>(); 
            sprite.transform.rotation = Quaternion.Euler(90, param2.Rotation, 0);
            sprite.sprite = _roomSpritesConfig.GetSprite(param2.Type);
            var model = _container.Instantiate<Room>(new object[]{ param1, param2 });
            return model;
        }
    }
}