using Common.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyIdleMove : EnemyMove
    {
        private readonly Enemy _enemy;
        private readonly EnemyMoveConfig _config;

        public EnemyIdleMove(EnemyMoveConfig config, IAnimation animation, Enemy enemy) : base(config, animation)
        {
            _config = config;
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.IsIdle = true;
        }

        public override void Exit()
        {
            base.Exit();
            _enemy.IsIdle = false;
        }
    }
}