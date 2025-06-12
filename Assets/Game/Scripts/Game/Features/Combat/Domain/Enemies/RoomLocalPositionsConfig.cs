using System;
using UnityEngine;

namespace Game.Features.Combat.Domain.Enemies
{
    public class RoomLocalPositionsConfig : ScriptableObject
    {
        [field: SerializeField] public Vector2 MiddleEnemyPos {get; private set;}
        [field: SerializeField] public Vector2 LeftEnemyPos {get; private set;}
        [field: SerializeField] public Vector2 RightEnemyPos {get; private set;}

        public Vector2 Get(EnemyPosition position) => position switch
        {
            EnemyPosition.Middle => MiddleEnemyPos,
            EnemyPosition.Left => LeftEnemyPos,
            EnemyPosition.Right => RightEnemyPos,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}