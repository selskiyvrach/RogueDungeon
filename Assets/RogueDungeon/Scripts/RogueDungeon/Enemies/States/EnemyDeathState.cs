using Common.Animations;
using Common.Fsm;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.States
{
    public class EnemyDeathState : EnemyState
    {
        private readonly Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public EnemyDeathState(EnemyStateConfig config, IAnimator animator, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animator)
        {
            _enemiesRegistry = enemiesRegistry;
            _enemy = enemy;
        }

        public override void CheckTransitions(ITypeBasedStateChanger stateChanger)
        {
            // no transitions from death...
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if (!IsFinished) 
                return;
            
            _enemiesRegistry.UnregisterEnemy(_enemy);
            _enemy.Disable();
            _enemy.Destroy();
        }
    }
}