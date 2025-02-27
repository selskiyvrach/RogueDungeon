using System.Linq;
using Common.Fsm;
using Common.UtilsDotNet;
using RogueDungeon.Combat;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindAttackState : HiveMindState
    {
        private readonly IEnemiesRegistry _enemiesRegistry;
        private Enemy _attackingEnemy;
        protected override bool IsSlackFrame => false;

        public HiveMindAttackState(HiveMindContext context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            for (var i = 0; i < Context.AttackQueue.Count; i++)
            {
                var enemy = Context.AttackQueue.Dequeue();
                if(_enemiesRegistry.Enemies.Any(n => n == enemy && n.IsAlive))
                    Context.AttackQueue.Enqueue(enemy);
            }
            foreach (var registeredEnemy in _enemiesRegistry.Enemies.Cast<Enemy>())
            {
                if(!Context.AttackQueue.Contains(registeredEnemy) && registeredEnemy.IsAlive)
                    Context.AttackQueue.Enqueue(registeredEnemy);
            }

            _attackingEnemy = Context.AttackQueue.Dequeue().ThrowIfNull();
            Context.AttackQueue.Enqueue(_attackingEnemy);
            
            _attackingEnemy.AttackBehaviour.StartAttack();
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(!_attackingEnemy.IsAlive || !_attackingEnemy.AttackBehaviour.IsAttacking)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}