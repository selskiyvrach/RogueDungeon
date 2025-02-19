using Common.Animations;
using RogueDungeon.Combat;

namespace RogueDungeon.Weapons
{
    public class EnemyAttackMove : AttackMove
    {
        private readonly EnemyAttackMoveConfig _config;
        
        public EnemyAttackMove(EnemyAttackMoveConfig config, IAnimator animator) : base(config, animator) => 
            _config = config;

        protected override void HandleAttack(IAttacksMediator attacksMediator) => 
            attacksMediator.MediateEnemyAttack(_config);
    }
}