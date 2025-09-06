using System.Collections.Generic;
using System.Linq;
using Game.Features.Levels.Domain;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Factories
{
    public class LevelFactory : IFactory<ILevelConfig, Level>
    {
        private readonly IFactory<IRoomConfig, RoomAdjacencyInfo, Room> _roomFactory;
        private readonly List<NeighborDirection> _roomNeighborsBuffer = new();

        public LevelFactory(IFactory<IRoomConfig, RoomAdjacencyInfo, Room> roomFactory) => 
            _roomFactory = roomFactory;

        public Level Create(ILevelConfig param1)
        {
            var roomCoords = param1.Rooms.Select(n => n.Coordinates).ToHashSet();
            var rooms = param1.Rooms.Select(n =>
            {
                _roomNeighborsBuffer.Clear();
                if(roomCoords.Contains(n.Coordinates + Vector2Int.up))
                    _roomNeighborsBuffer.Add(NeighborDirection.Top);
                if(roomCoords.Contains(n.Coordinates + Vector2Int.down))
                    _roomNeighborsBuffer.Add(NeighborDirection.Bottom);
                if(roomCoords.Contains(n.Coordinates + Vector2Int.left))
                    _roomNeighborsBuffer.Add(NeighborDirection.Left);
                if(roomCoords.Contains(n.Coordinates + Vector2Int.right))
                    _roomNeighborsBuffer.Add(NeighborDirection.Right);
                return _roomFactory.Create(n, new RoomAdjacencyInfo(_roomNeighborsBuffer));
            }).ToDictionary(n => n.Coordinates, n => n);
            var level = new Level(rooms[Vector2Int.zero], rooms);
            return level;
        }
    }
}