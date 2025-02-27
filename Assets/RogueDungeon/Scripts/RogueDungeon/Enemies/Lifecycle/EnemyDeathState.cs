using Common.Animations;
using Common.Fsm;
using RogueDungeon.Combat;

namespace RogueDungeon.Enemies
{
    public class EnemyDeathState : EnemyLifeCycleState
    {
        private readonly Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly EnemyLifeCycleConfig _config;
        protected override AnimationData Animation => new(_config.DeathAnimation.name, _config.DeathDuration);

        public EnemyDeathState(EnemyLifeCycleConfig config, IAnimator animator, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animator)
        {
            _config = config;
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