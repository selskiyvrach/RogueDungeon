using System;
using System.Linq;
using Game.Features.Levels.App;
using Game.Features.Levels.Domain;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Factories
{
    public class LevelFactory : ILevelFactory, ILevelCreatedEventDispatcher
    {
        private readonly DiContainer _container;
        private readonly IRoomFactory _roomFactory;

        public event Action<Level> OnLevelCreated;

        public LevelFactory(IRoomFactory roomFactory, DiContainer container)
        {
            _roomFactory = roomFactory;
            _container = container;
        }

        public Level Create(ILevelConfig param1)
        {
            var rooms = param1.Rooms.Select(n => _roomFactory.Create(n)).ToDictionary(n => n.Coordinates, n => n);
            var level = new Level(rooms[Vector2Int.zero], rooms);
            _container.Bind<Level>().FromInstance(level).AsSingle();
            OnLevelCreated?.Invoke(level);
            return level;
        }
    }
}