using Common.Animations;
using Common.Time;
using RogueDungeon.Enemies.HiveMind;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathMove : EnemyMove
    {
        private readonly Ticker _ticker = new();
        private readonly Enemy _enemy;
        private readonly IEnemiesRegistry _enemiesRegistry;
        public EnemyDeathMove(EnemyMoveConfig config, IAnimation animation, IEnemiesRegistry enemiesRegistry, Enemy enemy) : base(config, animation)
        {
            _enemiesRegistry = enemiesRegistry;
            _enemy = enemy;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_enemy.IsAlive;

        public override void Enter()
        {
            base.Enter();
            _ticker.Start(Tick);
        }

        public override void Exit()
        {
            base.Exit();
            _ticker.Stop();
        }

        private void Tick(float timeDelta)
        {
            if (!IsFinished) 
                return;
            
            _enemiesRegistry.UnregisterEnemy(_enemy);
            _enemy.Disable();
            _enemy.Destroy();
        }
    }
}