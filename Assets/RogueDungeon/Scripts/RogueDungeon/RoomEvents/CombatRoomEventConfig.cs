using System;
using Enemies;
using Levels;
using UnityEngine;

namespace RoomEvents
{
    public class CombatRoomEventConfig : RoomEventConfig
    {
        public override Type EventType => typeof(CombatRoomEvent);
        [field: SerializeField] public EnemyConfig MiddleEnemy {get; private set;}
        [field: SerializeField] public EnemyConfig LeftEnemy {get; private set;}
        [field: SerializeField] public EnemyConfig RightEnemy {get; private set;}
    }
}