using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using Common.UtilsDotNet;
using RogueDungeon.Enemies.MoveSet;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindAttackState : HiveMindState
    {
        private readonly List<EnemyAttackMove> _attacksBuffer = new();
        private readonly HiveMind _context;
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly HiveMind _hiveMind;
        private Enemy _attacker;

        public HiveMindAttackState(HiveMind context, IEnemiesRegistry enemiesRegistry, HiveMind hiveMind)
        {
            _context = context;
            _enemiesRegistry = enemiesRegistry;
            _hiveMind = hiveMind;
        }

        public override void Enter()
        {
            _hiveMind.SlackTime = 0;
            base.Enter();
            for (var i = 0; i < _context.AttackersQueue.Count; i++)
            {
                var enemy = _context.AttackersQueue.Dequeue();
                if(_enemiesRegistry.Enemies.Any(n => n == enemy && n.IsAlive && n.IsIdle))
                    _context.AttackersQueue.Enqueue(enemy);
            }
            
            foreach (var registeredEnemy in _enemiesRegistry.Enemies)
            {
                if(!_context.AttackersQueue.Contains(registeredEnemy) && registeredEnemy.IsAlive && registeredEnemy.IsIdle)
                    _context.AttackersQueue.Enqueue(registeredEnemy);
            }
            
            var attemptsLeft = _context.AttackersQueue.Count;
            while (attemptsLeft-- > 0)
            {
                var peek = _context.AttackersQueue.Peek().ThrowIfNull();
                _attacksBuffer.Clear();
                _attacksBuffer.AddRange(peek.Attacks.Where(n => n.IsSuitableForPosition(peek.TargetablePosition)));
                if(_attacksBuffer.Count == 0)
                    continue;

                _attacker = _context.AttackersQueue.Dequeue();
                _context.AttackersQueue.Enqueue(_attacker);
                _attacker.PerformMove(_attacksBuffer.Random());
                break;
            }
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_attacker?.IsIdle ?? true)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}