using System.Collections.Generic;
using System.Linq;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Levels
{
    public class LevelFactory : IFactory<LevelConfig, Level>
    {
        private readonly DiContainer _container;
        private readonly IFactory<RoomConfig, Room> _roomFactory;

        public LevelFactory(DiContainer container, IFactory<RoomConfig, Room> roomFactory)
        {
            _container = container;
            _roomFactory = roomFactory;
        }

        public Level Create(LevelConfig config)
        {
            var rooms = config.Rooms.Select(n => _roomFactory.Create(n)).ToDictionary(n => n.Coordinates, n => n);
            foreach (var room in rooms.Values)
            {
                var adjacentRooms = new List<Room>(4);
                if(rooms.TryGetValue(room.Coordinates + Vector2Int.up, out var top))
                    adjacentRooms.Add(top);
                if(rooms.TryGetValue(room.Coordinates + Vector2Int.down, out var bottom))
                    adjacentRooms.Add(bottom);
                if(rooms.TryGetValue(room.Coordinates + Vector2Int.left, out var left))
                    adjacentRooms.Add(left);
                if(rooms.TryGetValue(room.Coordinates + Vector2Int.right, out var right))
                    adjacentRooms.Add(right);
                room.AdjacentRooms = new AdjacentRooms(room, adjacentRooms);
            }

            var level = new Level(rooms[Vector2Int.zero], rooms.Values);
            _container.InstanceSingle(level);
            return _container.Resolve<Level>();
        }
    }
}