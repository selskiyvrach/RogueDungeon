using System.Linq;
using Game.Features.Levels.Domain;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Factories
{
    public class LevelFactory : IFactory<ILevelConfig, Level>
    {
        private readonly IRoomFactory _roomFactory;

        public LevelFactory(IRoomFactory roomFactory) => 
            _roomFactory = roomFactory;

        public Level Create(ILevelConfig param1)
        {
            var rooms = param1.Rooms.Select(n => _roomFactory.Create(n)).ToDictionary(n => n.Coordinates, n => n);
            var level = new Level(rooms[Vector2Int.zero], rooms);
            return level;
        }
    }
}