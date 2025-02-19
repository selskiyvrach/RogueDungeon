using Common.Animations;
using RogueDungeon.Combat;

namespace RogueDungeon.Weapons
{
    public class EnemyAttackMove : AttackMove
    {
        private readonly EnemyAttackMoveConfig _config;
        
        public EnemyAttackMove(EnemyAttackMoveConfig config, IAnimator animator, IAttacksMediator mediator) : base(config, animator, mediator) => 
            _config = config;

        protected override void HandleAttack(IAttacksMediator attacksMediator) => 
            attacksMediator.MediateEnemyAttack(_config);
    }
}