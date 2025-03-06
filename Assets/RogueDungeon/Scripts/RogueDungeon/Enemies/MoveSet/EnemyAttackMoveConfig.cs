using System;
using UnityEngine;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyAttackMoveConfig : EnemyMoveConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public EnemyAttackDirection AttackDirection { get; private set; }

        [field: SerializeField] public EnemyPosition[] SuitableForPositions { get; private set; } = { EnemyPosition.Middle };
        public override Type MoveType => typeof(EnemyAttackMove);
    }
}