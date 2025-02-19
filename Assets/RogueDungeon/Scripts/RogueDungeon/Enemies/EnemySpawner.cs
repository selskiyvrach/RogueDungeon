using System;
using RogueDungeon.Combat;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly EnemyParents _parents;
        private readonly IFactory<EnemyFactoryArgs, Enemy> _factory;
        private readonly IEnemiesRegistry _enemiesRegistry;

        public EnemySpawner(EnemyParents parents, IFactory<EnemyFactoryArgs, Enemy> factory, IEnemiesRegistry enemiesRegistry)
        {
            _parents = parents;
            _factory = factory;
            _enemiesRegistry = enemiesRegistry;
        }

        public void Spawn(EnemySpawningArgs args)
        {
            var position = args.Position;
            var parent = position switch
            {
                EnemyPosition.Middle => _parents.MiddleParent,
                EnemyPosition.Left => _parents.LeftParent,
                EnemyPosition.Right => _parents.RightParent,
                _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
            };
            var enemy = _factory.Create(new EnemyFactoryArgs(args.Config, parent));
            enemy.Position = position;
            _enemiesRegistry.RegisterEnemy(enemy);
        }
    }
}