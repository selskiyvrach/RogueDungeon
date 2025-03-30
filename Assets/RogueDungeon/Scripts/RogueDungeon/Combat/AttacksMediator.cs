using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Items;
using RogueDungeon.Player.Model;

namespace RogueDungeon.Combat
{
    public class AttacksMediator : IPlayerAttacksMediator, IEnemyAttacksMediator
    {
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly IPlayerRegistry _playerRegistry;
        private readonly CombatFeedbackPlayer _combatFeedbackPlayer;

        public AttacksMediator(IEnemiesRegistry enemiesRegistry, IPlayerRegistry playerRegistry, CombatFeedbackPlayer combatFeedbackPlayer)
        {
            _enemiesRegistry = enemiesRegistry;
            _playerRegistry = playerRegistry;
            _combatFeedbackPlayer = combatFeedbackPlayer;
        }
        
        public void MediatePlayerAttack(IWeapon weapon)
        {
            if (_enemiesRegistry.Enemies.FirstOrDefault(n => n.TargetablePosition == EnemyPosition.Middle) is not {} enemy)
            {
                // miss
                return;
            }

            enemy.TakeDamage(weapon.Damage, weapon.PoiseDamage);
            _combatFeedbackPlayer.OnHit();
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

            if (player.BlockerHandler.IsBlocking)
            {
                player.BlockerHandler.PerformBlock(damage, out var damageAfterBlock);
                damage = damageAfterBlock;
            }

            player.TakeHitDamage(damage);
            _combatFeedbackPlayer.OnHit();
        }
    }
}