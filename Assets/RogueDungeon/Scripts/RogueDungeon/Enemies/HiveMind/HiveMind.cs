using System.Collections.Generic;
using System.Linq;
using Common.Lifecycle;
using Common.UtilsDotNet;
using RogueDungeon.Enemies.States;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMind : IInitializable
    {
        private readonly IEnemiesRegistry _enemiesRegistry;
        private List<Enemy> _enemies => _enemiesRegistry.Enemies;
        private readonly List<(Enemy enemy, IEnumerable<EnemyMoveConfig> moves)?> _buffer = new(3);

        public HiveMind(IEnemiesRegistry enemiesRegistry) => 
            _enemiesRegistry = enemiesRegistry;

        public void Initialize()
        {
        }

        public void Tick(float deltaTime)
        {
            PruneDeadAndTickAlive(deltaTime);
            FillMiddlePositionIfEmpty();
            StartAttacks();
        }

        private void StartAttacks()
        {
            _buffer.Clear();
            foreach (var enemy in _enemies)
            {
                if(enemy.IsDoingMove)
                    return;
                if(enemy.IsIdle && enemy.HasMovesForCurrentPosition(out var moves))
                    _buffer.Add((enemy, moves));
            }

            var attacker = _buffer.RandomOrDefault();
            attacker?.enemy.StartMove(attacker.Value.moves.ToList().RandomOrDefault());
        }

        private void FillMiddlePositionIfEmpty()
        {
            if(_enemiesRegistry.Enemies.All(n => n.OccupiedPosition != EnemyPosition.Middle) && _enemiesRegistry.Enemies.FirstOrDefault(n => n.IsIdle) is {} enemy)
                enemy.ChangePosition(EnemyPosition.Middle);
        }

        private void PruneDeadAndTickAlive(float deltaTime)
        {
            for (var i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
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