using System;
using System.Linq;
using Game.Features.Combat.Domain.Enemies;
using Game.Features.Combat.Domain.Enemies.HiveMind;
using Game.Libs.Combat;
using Libs.Utils.DotNet;

namespace Game.Features.Combat.Domain
{
    public class AttacksMediator : ICombatEvents
    {
        private readonly HiveMind _hiveMind;

        public IPlayerDefenderInfoProvider PlayerDefenderInfoProvider { get; set; }
        public event Action<Enemy, PlayerAttackResult> OnPlayerAttackResult;
        public event Action<Enemy, EnemyAttackResult> OnEnemyAttackResult;

        public AttacksMediator(HiveMind hiveMind) => 
            _hiveMind = hiveMind;

        public void MediatePlayerAttack(PlayerAttackInfo playerAttackInfo)
        {
            var enemy = _hiveMind.Enemies.FirstOrDefault(n => n.TargetablePosition == playerAttackInfo.TargetPosition);
            OnPlayerAttackResult?.Invoke(enemy, new PlayerAttackResult(
                isHit: enemy != null,
                finalDamage: playerAttackInfo.Damage,
                finalPoiseDamage: playerAttackInfo.PoiseDamage, 
                attackInfo: playerAttackInfo));
        }

        public void MediateEnemyAttack(EnemyAttackInfo info, Enemy enemy)
        {
            info.Direction.ThrowIfNone();
            OnEnemyAttackResult?.Invoke(enemy, GetAttackResult(info, PlayerDefenderInfoProvider.GetDefenderInfo()));
        }

        private EnemyAttackResult GetAttackResult(EnemyAttackInfo info, DefenderInfo defenderInfo)
        {
            if (!defenderInfo.IsAlive)
                return EnemyAttackResult.NoResult(info);

            if (info.Direction == defenderInfo.DodgingAgainst)
                return info.Direction == AttackDirection.Left
                    ? EnemyAttackResult.DodgedRight(info)
                    : EnemyAttackResult.DodgedLeft(info);

            return defenderInfo.IsBlocking  
                ? EnemyAttackResult.BlockedHit(
                    staminaDamage: info.Damage * defenderInfo.BlockingStaminaCostFactor, 
                    healthDamage: info.Damage * (1- defenderInfo.BlockingAbsorbtion), info) 
                : EnemyAttackResult.NonBlockedHit(info.Damage, info);
        }
    }
}