using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using Common.Lifecycle;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMind : IInitializable
    {
        private readonly IEnemiesRegistry _enemiesRegistry;

        public List<Enemy> Enemies => _enemiesRegistry.Enemies;

        public HiveMind(IEnemiesRegistry enemiesRegistry) => 
            _enemiesRegistry = enemiesRegistry;

        public void Initialize()
        {
        }

        public void Tick(float deltaTime)
        {
            PruneDeadAndTickAlive(deltaTime);
            if(_enemiesRegistry.Enemies.All(n => n.OccupiedPosition != EnemyPosition.Middle) && _enemiesRegistry.Enemies.FirstOrDefault(n => n.IsIdle) is {} enemy)
                enemy.ChangePosition(EnemyPosition.Middle);
        }

        private void PruneDeadAndTickAlive(float deltaTime)
        {
            for (var i = Enemies.Count - 1; i >= 0; i--)
            {
                var enemy = Enemies[i];
                if (enemy.IsReadyToBeDisposed)
                {
                    _enemiesRegistry.UnregisterEnemy(enemy);
                    enemy.Destroy();
                    continue;
                }
                enemy.Tick(deltaTime);
            }
        }
    }
}