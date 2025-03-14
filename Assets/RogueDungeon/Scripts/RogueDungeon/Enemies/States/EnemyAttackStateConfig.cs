using System;
using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public class EnemyAttackStateConfig : EnemyMoveConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public EnemyAttackDirection AttackDirection { get; private set; }
        public override Type StateType => typeof(EnemyAttackState);
    }
}