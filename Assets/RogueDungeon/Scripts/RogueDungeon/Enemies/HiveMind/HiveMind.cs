using System.Collections.Generic;
using System.Linq;
using Common.Lifecycle;
using Common.UtilsDotNet;
using RogueDungeon.Enemies.States;
using UnityEngine;
using Random = Common.UtilsDotNet.Random;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMind : IInitializable
    {
        private readonly HiveMindConfig _config;
        private readonly IEnemiesRegistry _enemiesRegistry;
        private List<Enemy> _enemies => _enemiesRegistry.Enemies;
        private readonly List<(Enemy enemy, IEnumerable<EnemyMoveConfig> moves)> _buffer = new(3);

        public HiveMind(IEnemiesRegistry enemiesRegistry, HiveMindConfig config)
        {
            _enemiesRegistry = enemiesRegistry;
            _config = config;
        }

        public void Initialize()
        {
        }

        public void Tick(float deltaTime)
        {
            PruneDeadAndTickAlive(deltaTime);
            FillMiddlePositionIfEmpty();
            TickBattleHeat(deltaTime);
            StartAttacks();
        }

        private void TickBattleHeat(float timeDelta)
        {
            timeDelta *= Mathf.Pow(_config.BattleHeatFactorPerExtraEnemy, _enemies.Count - 1);
            if (_enemies.Any(n => n.IsDoingMove))
                timeDelta *= _config.BattleHeatFactorWhenTheresAnAttackGoing;
            foreach (var enemy in _enemies)
                enemy.TickBattleHeat(timeDelta);
        } 

        private void StartAttacks()
        {
            _buffer.Clear();
            foreach (var enemy in _enemies)
            {
                if(enemy.IsDoingMove)
                    return;
                if(enemy.IsIdle && enemy.CurrentAggression >= 1 && enemy.HasMovesForCurrentPosition(out var moves))
                    _buffer.Add((enemy, moves));
            }

            if(!_buffer.Any())
                return;
            _buffer.Sort((a, b) => a.enemy.CurrentAggression.CompareTo(b.enemy.CurrentAggression));
            var attacker = UnityEngine.Random.Range(0, 1) < 0.75f ? _buffer[^1] : _buffer[0];
            attacker.enemy.StartMove(attacker.moves.ToList().RandomOrDefault());
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