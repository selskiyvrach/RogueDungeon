using System;
using RogueDungeon.Enemies;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public class CombatRoomEventConfig : RoomEventConfig
    {
        public override Type EventType => typeof(CombatRoomEvent);
        [field: SerializeField] public EnemyConfig MiddleEnemy {get; private set;}
        [field: SerializeField] public EnemyConfig LeftEnemy {get; private set;}
        [field: SerializeField] public EnemyConfig RightEnemy {get; private set;}
    }
}