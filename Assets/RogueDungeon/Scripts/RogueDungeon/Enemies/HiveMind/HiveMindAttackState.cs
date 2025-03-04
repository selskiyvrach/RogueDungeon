using System.Collections.Generic;
using System.Linq;
using Common.Fsm;
using Common.UtilsDotNet;
using RogueDungeon.Enemies.Attacks;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindAttackState : HiveMindState
    {
        private readonly List<EnemyAttackAction> _attacksBuffer = new(); 
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly HiveMindContext _hiveMindContext;
        private Enemy _attackingEnemy;
        private EnemyAttackAction _attack;
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
                if(_enemiesRegistry.Enemies.Any(n => n == enemy && n.IsAlive))
                    Context.AttackersQueue.Enqueue(enemy);
            }
            foreach (var registeredEnemy in _enemiesRegistry.Enemies.Cast<Enemy>())
            {
                if(!Context.AttackersQueue.Contains(registeredEnemy) && registeredEnemy.IsAlive)
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

                _attackingEnemy = Context.AttackersQueue.Dequeue();
                Context.AttackersQueue.Enqueue(_attackingEnemy);
                _attack = _attacksBuffer.Random();
                _attack.Start();
                break;
            }
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_attack?.IsFinished ?? true)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}