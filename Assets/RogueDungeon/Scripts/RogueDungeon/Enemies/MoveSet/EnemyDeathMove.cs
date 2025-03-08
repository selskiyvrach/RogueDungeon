using Common.Animations;
using Common.Fsm;
using Common.Time;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathMove : EnemyMove
    {
        private Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public EnemyDeathMove(EnemyMoveConfig config, IAnimation animation, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animation)
        {
            _enemiesRegistry = enemiesRegistry;
            _enemy = enemy;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_enemy.IsAlive;
        
        public override void Tick(float timeDelta)
        {
            if (!IsFinished && _enemy == null)
                return;
            
            _enemiesRegistry.UnregisterEnemy(_enemy);
            _enemy.Destroy();
            _enemy = null;
        }
    }
}