using System.Linq;
using Common.Animations;
using RogueDungeon.Animations;

namespace RogueDungeon.Enemies.MoveSet
{
    
    // find a way to define enemy positions
        // target for player (either changes mid-move or became untargetable)
        // remove changing position from enemy positions
        // find a way to tell which enemy occupies which position for hive mind
            // maybe two properties
                // TargetablePosition (None if moving\birthing\dying)
                // occupied position (to prevent confusion for hivemind)
                
    // create from any transitions for stun and death (from any except maybe for stun to exclude death)
    // create enemy moveset extension mechanism
    
    
    
    public class EnemyAttackMove : EnemyMove
    {
        private readonly IEnemyAttacksMediator _mediator;
        private readonly EnemyAttackMoveConfig _config;
        
        public EnemyAttackMove(EnemyAttackMoveConfig config, IAnimation animation, IEnemyAttacksMediator mediator) : base(config, animation)
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