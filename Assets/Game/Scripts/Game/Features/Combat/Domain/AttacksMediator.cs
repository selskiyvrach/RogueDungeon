using System;
using System.Linq;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;
using Libs.Utils.DotNet;

namespace Game.Features.Combat.Domain
{
    public class AttacksMediator 
    {
        private readonly HiveMind _hiveMind;

        public IPlayerDefenderInfoProvider PlayerDefenderInfoProvider { get; set; }
        public event Action<PlayerAttackResult> OnPlayerAttackResult;
        public event Action<EnemyAttackResult> OnEnemyAttackResult;

        public AttacksMediator(HiveMind hiveMind) => 
            _hiveMind = hiveMind;

        public void MediatePlayerAttack(PlayerAttackInfo playerAttackInfo) => 
            OnPlayerAttackResult?.Invoke(new PlayerAttackResult(
                isHit: _hiveMind.Enemies.FirstOrDefault(n => n.TargetablePosition == playerAttackInfo.TargetPosition) is {} enemy, 
                finalDamage: playerAttackInfo.Damage, 
                finalPoiseDamage: playerAttackInfo.PoiseDamage));

        public void MediateEnemyAttack(EnemyAttackInfo info)
        {
            info.Direction.ThrowIfNone();
            OnEnemyAttackResult?.Invoke(GetAttackResult(info, PlayerDefenderInfoProvider.GetDefenderInfo()));
        }

        private EnemyAttackResult GetAttackResult(EnemyAttackInfo info, DefenderInfo defenderInfo)
        {
            if (!defenderInfo.IsAlive)
                return EnemyAttackResult.NoResult;

            if (info.Direction == defenderInfo.DodgingAgainst)
                return info.Direction == AttackDirection.Left
                    ? EnemyAttackResult.DodgedRight
                    : EnemyAttackResult.DodgedLeft;

            return defenderInfo.IsBlocking  
                ? EnemyAttackResult.BlockedHit(
                    staminaDamage: info.Damage * defenderInfo.BlockingStaminaCostFactor, 
                    healthDamage: info.Damage * (1- defenderInfo.BlockingAbsorbtion)) 
                : EnemyAttackResult.NonBlockedHit(info.Damage);
        }
    }
}