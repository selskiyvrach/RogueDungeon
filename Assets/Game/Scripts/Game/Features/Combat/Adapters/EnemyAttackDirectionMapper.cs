using System;
using Game.Features.Combat.Domain;

namespace Game.Features.Combat.Adapters
{
    public static class EnemyAttackDirectionMapper
    {
        public static EnemyAttackDirection ToDomain(this Enemies.Domain.EnemyAttackDirection direction) =>
            direction switch
            {
                Enemies.Domain.EnemyAttackDirection.Left => EnemyAttackDirection.Left,
                Enemies.Domain.EnemyAttackDirection.Right => EnemyAttackDirection.Right,
                Enemies.Domain.EnemyAttackDirection.Center => EnemyAttackDirection.Center,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}