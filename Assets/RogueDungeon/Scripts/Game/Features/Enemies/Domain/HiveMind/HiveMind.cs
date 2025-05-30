using System.Collections.Generic;
using System.Linq;

namespace Game.Features.Enemies.Domain.HiveMind
{
    public class HiveMind
    {
        private readonly HiveMindCombo _combo;
        private readonly HiveMindConfig _config;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public List<Enemy> Enemies => _enemiesRegistry.Enemies;

        public HiveMind(IEnemiesRegistry enemiesRegistry, HiveMindConfig config)
        {
            _enemiesRegistry = enemiesRegistry;
            _config = config;
            _combo = new HiveMindCombo(this);
        }

        public void Tick(float deltaTime)
        {
            PruneDeadAndTickAlive(deltaTime);
            if(!_combo.IsRunning)
                FillMiddlePositionIfEmpty();
            _combo.Tick(deltaTime);
        }

        private void FillMiddlePositionIfEmpty()
        {
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