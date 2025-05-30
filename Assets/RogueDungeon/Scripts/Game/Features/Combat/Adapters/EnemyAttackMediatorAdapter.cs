using Game.Features.Combat.Domain;
using Game.Features.Enemies.Domain;
using OuterAttackDirection = Game.Features.Enemies.Domain.EnemyAttackDirection;

namespace Game.Features.Combat.Adapters
{
    public class EnemyAttackMediatorAdapter : IEnemyAttacksMediator
    {
        private readonly AttacksMediator _attacksMediator;

        public EnemyAttackMediatorAdapter(AttacksMediator attacksMediator) => 
            _attacksMediator = attacksMediator;

        public void MediateEnemyAttack(float damage, OuterAttackDirection attackDirection) => 
            _attacksMediator.MediateEnemyAttack(damage, attackDirection.ToDomain());
    }
}