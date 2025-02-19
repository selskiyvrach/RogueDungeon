using System;
using RogueDungeon.Combat;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    public class EnemyAttackMoveConfig : AttackMoveConfig, IEnemyAttackInfo
    {
        [field: SerializeField] public EnemyAttackDirection AttackDirection { get; private set; }
        public override Type MoveType => typeof(EnemyAttackMove);
    }
}