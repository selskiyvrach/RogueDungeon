using System;
using Game.Features.Combat.Domain.Enemies;
using Game.Features.Combat.Domain.Enemies.Moves;
using Libs.Movesets;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemyMoveIdsToMoveTypesConverter : IMoveIdToTypeConverter
    {
        public Type GetMoveType(string id) =>
            id switch
            {
                MoveNames.IDLE => typeof(EnemyIdleMove),
                MoveNames.STAGGER => typeof(EnemyStaggerMove),
                MoveNames.BIRTH => typeof(EnemyBirthMove),
                MoveNames.DEATH => typeof(EnemyDeathMove),
                MoveNames.MOVE => typeof(EnemyMovementMove),
                MoveNames.ATTACK_LEFT => typeof(EnemyAttackMove),
                MoveNames.ATTACK_RIGHT => typeof(EnemyAttackMove),
                MoveNames.ATTACK_MIDDLE => typeof(EnemyAttackMove),
                _ => throw new ArgumentOutOfRangeException(nameof(id), id, null)
            };
    }
}