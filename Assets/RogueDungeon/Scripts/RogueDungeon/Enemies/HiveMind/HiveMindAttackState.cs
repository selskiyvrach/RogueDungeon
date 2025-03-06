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
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly HiveMindContext _hiveMindContext;
        private Enemy _attacker;
        protected override bool IsSlackFrame => false;

        public HiveMindAttackState(HiveMindContext context, IEnemiesRegistry enemiesRegistry, HiveMindContext hiveMindContext) : base(context)
        {
            _enemiesRegistry = enemiesRegistry;
            _hiveMindContext = hiveMindContext;
        }

        public override void Enter()
        {
            _hiveMindContext.SlackTime = 0;
            base.Enter();
            for (var i = 0; i < Context.AttackersQueue.Count; i++)
            {
                var enemy = Context.AttackersQueue.Dequeue();
                if(_enemiesRegistry.Enemies.Any(n => n == enemy && n.IsAlive && n.IsIdle))
                    Context.AttackersQueue.Enqueue(enemy);
            }
            foreach (var registeredEnemy in _enemiesRegistry.Enemies)
            {
                if(!Context.AttackersQueue.Contains(registeredEnemy) && registeredEnemy.IsAlive && registeredEnemy.IsIdle)
                    Context.AttackersQueue.Enqueue(registeredEnemy);
            }
            
            var attemptsLeft = Context.AttackersQueue.Count;
            while (attemptsLeft-- > 0)
            {
                var peek = Context.AttackersQueue.Peek().ThrowIfNull();
                _attacksBuffer.Clear();
                _attacksBuffer.AddRange(peek.Attacks.Where(n => n.IsSuitableForPosition(peek.CombatPosition)));
                if(_attacksBuffer.Count == 0)
                    continue;

                _attacker = Context.AttackersQueue.Dequeue();
                Context.AttackersQueue.Enqueue(_attacker);
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