using System;
using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public class EnemyMoveConfig : EnemyStateConfig
    {
        [field: SerializeField] public EnemyPosition SuitableForPositions { get; private set; }
        public override Type StateType => typeof(EnemyMovementState);
    }
}