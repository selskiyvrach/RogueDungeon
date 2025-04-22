using Common.Animations;
using RogueDungeon.Animations;

namespace RogueDungeon.Enemies.States
{
    public class EnemyAttackMove : EnemyMove
    {
        private readonly Enemy _enemy;
        private readonly EnemyAttackMoveConfig _config;
        private readonly IEnemyAttacksMediator _mediator;
        public override Priority Priority => Priority.Attack;
        protected override float Duration => _config.Duration;
        
        public EnemyAttackMove(EnemyAttackMoveConfig config, Enemy enemy, IAnimation animation, IEnemyAttacksMediator mediator, string id) : base(animation, id)
        {
            _config = config;
            _enemy = enemy;
            _mediator = mediator;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.Hit) 
                _mediator.MediateEnemyAttack(_config.Damage, _config.Direction);
        }
    }
}