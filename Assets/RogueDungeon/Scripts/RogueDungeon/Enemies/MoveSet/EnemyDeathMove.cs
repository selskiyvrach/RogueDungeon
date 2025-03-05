using Common.Animations;
using Common.Fsm;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathMove : EnemyMove
    {
        private readonly Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public EnemyDeathMove(EnemyMoveConfig config, IAnimator animator, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animator)
        {
            _enemiesRegistry = enemiesRegistry;
            _enemy = enemy;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_enemy.IsAlive;

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