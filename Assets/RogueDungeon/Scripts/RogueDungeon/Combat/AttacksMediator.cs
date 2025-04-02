using System.Linq;
using Common.UtilsDotNet;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Items;
using RogueDungeon.Player.Model;
using UnityEngine;

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

            var modifier = _playerRegistry.Player.Hands.IsDoubleGrip ? _playerRegistry.Player.DoubleGripDamageBonus : 1f;

            enemy.TakeDamage(weapon.Damage * modifier, weapon.PoiseDamage * modifier);
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

            if (player is { IsBlocking: true})
            {
                player.HasUnabsorbedBlockImpact = true;
                var doubleGripBonus =
                    player.Hands.IsDoubleGrip && player.Hands.ThisHand(player.BlockingItem) == player.Hands.LeftHand
                        ? player.DoubleGripBlockBonus
                        : 1;
            
                var staminaCost = damage * player.BlockingItem.BlockStaminaCostMultiplier / doubleGripBonus;
                player.Stamina.AddDelta(- staminaCost);
                damage = Mathf.Clamp(damage - player.Stamina.Current / player.BlockingItem.BlockStaminaCostMultiplier, 0, float.PositiveInfinity);
            }

            player.Health.AddDelta(- damage);
            _combatFeedbackPlayer.OnHit();
        }
    }
}