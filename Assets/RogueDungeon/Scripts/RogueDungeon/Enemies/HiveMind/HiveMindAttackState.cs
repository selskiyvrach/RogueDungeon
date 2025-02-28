using System.Linq;
using Common.Fsm;
using Common.UtilsDotNet;
using RogueDungeon.Combat;
using RogueDungeon.Enemies.Attacks;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindAttackState : HiveMindState
    {
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly HiveMindContext _hiveMindContext;
        private Enemy _attackingEnemy;
        private EnemyAction _attack;
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
            
            var attemptsLeft = Context.AttackQueue.Count;
            while (attemptsLeft-- > 0)
            {
                var peek = Context.AttackQueue.Peek();
                if (peek.Actions.FirstOrDefault(n =>
                        n.Type == EnemyActionType.LightAttack &&
                        ((EnemyAttackAction)n).IsSuitableForPosition(peek.CombatPosition)) is not { } attack) 
                    continue;
                
                _attackingEnemy = Context.AttackQueue.Dequeue().ThrowIfNull();
                _attack = attack;
                _attack.Start();
                break;
            }

            Context.AttackQueue.Enqueue(_attackingEnemy);
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            if(_attack?.IsFinished ?? true)
                stateChanger.ChangeState<HiveMindIdleState>();
        }
    }
}