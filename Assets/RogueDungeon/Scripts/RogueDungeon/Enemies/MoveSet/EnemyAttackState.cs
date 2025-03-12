using Common.Animations;
using RogueDungeon.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyAttackState : EnemyState
    {
        private readonly IEnemyAttacksMediator _mediator;
        private readonly EnemyAttackStateConfig _config;

        public EnemyAttackState(EnemyAttackStateConfig config, IAnimation animation, IEnemyAttacksMediator mediator) : base(config, animation)
        {
            _config = config;
            _mediator = mediator;
        }

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.Hit) 
                _mediator.MediateEnemyAttack(_config.Damage, _config.AttackDirection);
        }
    }
}