using System.Collections.Generic;
using System.Linq;
using Common.Lifecycle;
using Common.UtilsDotNet;
using RogueDungeon.Enemies.States;
using Random = UnityEngine.Random;

namespace RogueDungeon.Enemies
{
    public class EnemyCombo : ITickable
    {
        private readonly Enemy _enemy;
        private readonly List<EnemyMoveConfig> _moves = new();
        private readonly List<EnemyMoveConfig> _movesPool = new();
        private int _currentMoveIndex;

        private EnemyConfig.ComboPerPositionInfo _currentPositionComboInfo => _enemy.Config.Combos.First(n => n.Position == _enemy.OccupiedPosition);
        
        public bool IsRunning { get; private set; }
        public float TimeChilling { get; private set; }
        public float TargetChillTime => _currentPositionComboInfo.ChillTime;
        
        public EnemyCombo(Enemy enemy) => 
            _enemy = enemy;

        public void Tick(float timeDelta)
        {
            if (!IsRunning)
            {
                TimeChilling += timeDelta;
                return;
            }

            if(_enemy.IsStaggeredOrDead)
                Cancel();

            if (!_enemy.IsIdle)
                return;
            
            if(_currentMoveIndex >= _moves.Count)
                Cancel();
                
            _enemy.StartMove(_moves[_currentMoveIndex++]);
        }

        public void StartNewCombo()
        {
            if(!_enemy.HasMovesForCurrentPosition(out var moves))
                return;

            _moves.Clear();
            _movesPool.Clear();
            _movesPool.AddRange(moves);
            var count = _currentPositionComboInfo.ComboCount;
            var movesCount = Random.Range(count.x, count.y + 1);
            while(_moves.Count < movesCount)
                _moves.Add(_movesPool.Random());
            
            IsRunning = true;
            TimeChilling = 0;
            _currentMoveIndex = 0;
        }

        private void Cancel() => 
            IsRunning = false;
    }
}