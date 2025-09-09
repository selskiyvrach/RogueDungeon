using Libs.Animations;

namespace Game.Features.Combat.Domain.Enemies.Moves
{
    public class EnemyIdleMove : EnemyMove
    {
        private readonly Enemy _enemy;
        protected override bool IsLooping => true;
        public override Priority Priority => Priority.Idle;
        protected override float Duration => _enemy.Config.IdleAnimationDuration;

        public EnemyIdleMove(IAnimation animation, Enemy enemy, string id) : base(animation, id) => 
            _enemy = enemy;

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