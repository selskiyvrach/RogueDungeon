using Common.Animations;
using Common.Fsm;
using Common.Time;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathMove : EnemyMove, ITickableState
    {
        private readonly Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public EnemyDeathMove(EnemyMoveConfig config, IAnimation animation, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animation)
        {
            _enemiesRegistry = enemiesRegistry;
            _enemy = enemy;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_enemy.IsAlive;
        
        public void Tick(float timeDelta)
        {
            if (!IsFinished) 
                return;
            
            _enemiesRegistry.UnregisterEnemy(_enemy);
            _enemy.Disable();
            _enemy.Destroy();
        }
    }
}