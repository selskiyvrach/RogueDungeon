using System;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;

namespace Game.Features.Combat.Domain
{
    public interface ICombatEvents
    {
        event Action<Enemy, PlayerAttackResult> OnPlayerAttackResult;
        event Action<Enemy, EnemyAttackResult> OnEnemyAttackResult;
    }
}