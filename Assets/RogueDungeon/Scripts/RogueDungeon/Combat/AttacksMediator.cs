using System.Linq;
using Common.UtilsDotNet;

namespace RogueDungeon.Combat
{
    public class AttacksMediator : IAttacksMediator
    {
        private readonly ICombatantsRegistry _registry;

        public AttacksMediator(ICombatantsRegistry registry) => 
            _registry = registry;

        public void MediatePlayerAttack(float damage)
        {
            if (_registry.Enemies.FirstOrDefault(n => n.CombatPosition == EnemyPosition.Middle) is not {} enemy)
            {
                // miss
                return;
            }

            enemy.TakeDamage(damage);
        }

        public void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection)
        {
            attackDirection.ThrowIfNone();
            if(_registry.Player is not {} player)
                return;

            if (attackDirection == EnemyAttackDirection.Left &&
                player.DodgeState == PlayerDodgeState.DodgingRight
                || attackDirection == EnemyAttackDirection.Right &&
                player.DodgeState == PlayerDodgeState.DodgingLeft)
            {
                // miss
                return;
            }
            
            player.TakeDamage(damage);
        }
    }
}