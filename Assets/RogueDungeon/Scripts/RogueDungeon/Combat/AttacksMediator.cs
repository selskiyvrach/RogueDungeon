using System.Linq;
using Common.UtilsDotNet;

namespace RogueDungeon.Combat
{
    public class AttacksMediator : IAttacksMediator
    {
        private readonly ICombatantsRegistry _registry;

        public AttacksMediator(ICombatantsRegistry registry) => 
            _registry = registry;

        public void MediatePlayerAttack(IPlayerAttackInfo attackInfo)
        {
            if (_registry.Enemies.FirstOrDefault(n => n.Position == EnemyPosition.Middle) is not {} enemy)
            {
                // miss
                return;
            }

            enemy.TakeDamage(attackInfo.Damage);
        }

        public void MediateEnemyAttack(IEnemyAttackInfo attackInfo)
        {
            attackInfo.AttackDirection.ThrowIfNone();
            if(_registry.Player is not {} player)
                return;

            if (attackInfo.AttackDirection == EnemyAttackDirection.Left &&
                player.DodgeState == PlayerDodgeState.DodgingRight
                || attackInfo.AttackDirection == EnemyAttackDirection.Right &&
                player.DodgeState == PlayerDodgeState.DodgingLeft)
            {
                // miss
                return;
            }
            
            player.TakeDamage(attackInfo.Damage);
        }
    }
}