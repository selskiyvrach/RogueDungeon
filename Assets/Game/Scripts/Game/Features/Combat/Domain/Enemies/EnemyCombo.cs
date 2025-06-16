using System.Collections.Generic;
using System.Linq;
using Game.Libs.Combat;
using Libs.Lifecycle;
using Libs.Utils.DotNet;
using Random = UnityEngine.Random;

namespace Game.Features.Combat.Domain.Enemies
{
    public class EnemyCombo : ITickable
    {
        private readonly Enemy _enemy;
        private readonly List<EnemyAttackMoveConfig> _moves = new();
        private readonly List<EnemyAttackMoveConfig> _movesPool = new();
        private int _currentMoveIndex;

        public bool IsRunning { get; private set; }
        public float TimeChilling { get; private set; }
        
        public EnemyCombo(Enemy enemy) => 
            _enemy = enemy;

        public void Tick(float timeDelta)
        {
            if (_enemy.IsStunnedOrDead)
            {
                Cancel();
                return;
            }
            
            if (!IsRunning)
            {
                TimeChilling += timeDelta;
                return;
            }

            if (!_enemy.IsIdle)
                return;

            if (_currentMoveIndex >= _moves.Count)
            {
                Cancel();
                return;
            }
                
            _enemy.StartMove(_moves[_currentMoveIndex++].Name);
        }

        public void StartNewCombo()
        {
            if(!_enemy.HasAttacksForCurrentPosition(out var moves))
                return;

            _moves.Clear();
            _movesPool.Clear();
            
            var targetMovesCount = _enemy.OccupiedPosition == EnemyPosition.Middle 
                ? Random.Range(_enemy.Config.FrontLineComboLength.x, _enemy.Config.FrontLineComboLength.y + 1) 
                : 1;
            
            while (_moves.Count < targetMovesCount)
            {
                if (!_movesPool.Any())
                {
                    _movesPool.AddRange(moves);
                    _movesPool.Shuffle();
                }
                _moves.AddRange(_movesPool, targetMovesCount - _moves.Count);
                _movesPool.Clear();
            }
            
            IsRunning = true;
            _currentMoveIndex = 0;
        }

        private void Cancel()
        {
            IsRunning = false;
            TimeChilling = 0;
        }
    }
}