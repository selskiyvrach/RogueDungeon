using System;
using RogueDungeon.Combat;
using RogueDungeon.Levels;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly RoomLocalPositionsConfig _roomLocalPositionConfig;
        private readonly EnemyParent _parents;
        private readonly IFactory<EnemyFactoryArgs, Enemy> _factory;
        private readonly IEnemiesRegistry _enemiesRegistry;

        public EnemySpawner(EnemyParent parents, IFactory<EnemyFactoryArgs, Enemy> factory, IEnemiesRegistry enemiesRegistry, RoomLocalPositionsConfig roomLocalPositionConfig)
        {
            _parents = parents;
            _factory = factory;
            _enemiesRegistry = enemiesRegistry;
            _roomLocalPositionConfig = roomLocalPositionConfig;
        }

        public void Spawn(EnemySpawningArgs args)
        {
            var position = args.Position;
            var enemy = _factory.Create(new EnemyFactoryArgs(args.Config, _parents.transform));
            enemy.CombatPosition = position;
            enemy.Transform.localPosition = enemy.CombatPosition switch
            {
                EnemyPosition.Middle => _roomLocalPositionConfig.MiddleEnemyPos,
                EnemyPosition.Left => _roomLocalPositionConfig.LeftEnemyPos,
                EnemyPosition.Right => _roomLocalPositionConfig.RightEnemyPos,
                _ => throw new ArgumentOutOfRangeException()
            };
            _enemiesRegistry.RegisterEnemy(enemy);
        }
    }
}