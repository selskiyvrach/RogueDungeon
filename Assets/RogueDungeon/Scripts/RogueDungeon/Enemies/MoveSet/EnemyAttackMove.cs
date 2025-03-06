using System.Linq;
using Common.Animations;
using RogueDungeon.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyAttackMove : EnemyMove
    {
        private readonly IEnemyAttacksMediator _mediator;
        private readonly EnemyAttackMoveConfig _config;
        
        public EnemyAttackMove(EnemyAttackMoveConfig config, IAnimator animator, IEnemyAttacksMediator mediator) : base(config, animator)
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

        public bool IsSuitableForPosition(EnemyPosition pos) => 
            _config.SuitableForPositions.Contains(pos);
    }
}