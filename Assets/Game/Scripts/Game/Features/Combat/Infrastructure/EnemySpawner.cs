using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;
using UnityEngine;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly RoomLocalPositionsConfig _roomLocalPositionConfig;
        private readonly IFactory<string, Transform, Enemy> _factory;
        
        public EnemySpawner(IFactory<string, Transform, Enemy> factory, RoomLocalPositionsConfig roomLocalPositionConfig)
        {
            _factory = factory;
            _roomLocalPositionConfig = roomLocalPositionConfig;
        }

        public Enemy Spawn(string id, EnemyPosition position, Transform parent)
        {
            var enemy = _factory.Create(id, parent);
            enemy.OccupiedPosition = position;
            enemy.WorldObject.LocalPosition = _roomLocalPositionConfig.Get(enemy.OccupiedPosition);
            return enemy;
        }
    }
}