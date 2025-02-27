using System;
using System.Linq;
using RogueDungeon.Combat;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly BattleField _parent;
        private readonly RoomLocalPositionsConfig _roomLocalPositionConfig;
        private readonly IFactory<EnemyFactoryArgs, Enemy> _factory;
        private readonly IEnemiesRegistry _enemiesRegistry;

        public EnemySpawner(BattleField parent, IFactory<EnemyFactoryArgs, Enemy> factory, IEnemiesRegistry enemiesRegistry, RoomLocalPositionsConfig roomLocalPositionConfig)
        {
            _parent = parent;
            _factory = factory;
            _enemiesRegistry = enemiesRegistry;
            _roomLocalPositionConfig = roomLocalPositionConfig;
        }

        public void Spawn(EnemyConfig config, EnemyPosition position)
        {
            if (_enemiesRegistry.Enemies.Any(n => n.CombatPosition == position))
                throw new Exception("Enemy position you want to spawn in is already occupied");
            
            var enemy = _factory.Create(new EnemyFactoryArgs(config, _parent.transform));
            enemy.CombatPosition = position;
            enemy.WorldObject.LocalPosition = _roomLocalPositionConfig.Get(enemy.CombatPosition);
            _enemiesRegistry.RegisterEnemy(enemy);
        }
    }
}