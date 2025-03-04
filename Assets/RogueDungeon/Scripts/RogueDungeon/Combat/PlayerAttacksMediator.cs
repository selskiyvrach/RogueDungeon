using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Player;

namespace RogueDungeon.Combat
{
    public class AttacksMediator : IPlayerAttacksMediator, IEnemyAttacksMediator
    {
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly IPlayerRegistry _playerRegistry;

        public AttacksMediator(IEnemiesRegistry enemiesRegistry, IPlayerRegistry playerRegistry)
        {
            _enemiesRegistry = enemiesRegistry;
            _playerRegistry = playerRegistry;
        }


        public void MediatePlayerAttack(float damage)
        {
            if (_enemiesRegistry.Enemies.FirstOrDefault(n => n.CombatPosition == EnemyPosition.Middle) is not {} enemy)
            {
                // miss
                return;
            }

            enemy.TakeDamage(damage);
        }

        public void MediateEnemyAttack(float damage, EnemyAttackDirection attackDirection)
        {
            attackDirection.ThrowIfNone();
            if(_playerRegistry.Player is not {} player)
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