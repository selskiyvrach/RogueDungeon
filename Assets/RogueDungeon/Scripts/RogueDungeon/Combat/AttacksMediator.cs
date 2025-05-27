using System.Linq;
using Common.UtilsDotNet;
using Enemies;
using Player.Model;
using RogueDungeon.Items.Model;
using UnityEngine;

namespace Combat
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

            var modifier = _playerRegistry.Player.Hands.IsDoubleGrip ? _playerRegistry.Player.DoubleGripDamageBonus : 1f;

            var severity = enemy.TakeDamage(weapon.Damage * modifier, weapon.PoiseDamage * modifier);
            _combatFeedbackPlayer.OnHit(severity);
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

            if (player is { BlockingItem: {} shield})
            {
                player.HasUnabsorbedBlockImpact = true;
                var doubleGripBonus = player.Hands.IsDoubleGrip 
                    ? player.DoubleGripBlockBonus
                    : 1;
            
                var staminaCost = damage * shield.BlockStaminaCostMultiplier / doubleGripBonus;
                damage = Mathf.Max(0, staminaCost - player.Stamina.Current) / shield.BlockStaminaCostMultiplier; 
                player.Stamina.AddDelta(- staminaCost);
            }

            player.Health.AddDelta(- damage);
            _combatFeedbackPlayer.OnHit(HitSeverity.Regular);
        }
    }
}