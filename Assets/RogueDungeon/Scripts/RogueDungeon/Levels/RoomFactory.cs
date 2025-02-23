using UnityEngine;
using Zenject;

namespace RogueDungeon.Levels
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

        public Room Create(RoomConfig config)
        {
            var presenter = Object.Instantiate(config.Prefab, _parent);
            var room = _container.Instantiate<Room>(new object[]{ config});
            presenter.transform.localPosition = new Vector3(room.Coordinates.x, 0, room.Coordinates.y);
            return room;
        }
    }
}