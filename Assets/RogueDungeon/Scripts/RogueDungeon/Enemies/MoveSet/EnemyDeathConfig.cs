using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathConfig : EnemyMoveConfig
    {
        public override Type MoveType => typeof(EnemyDeathMove);
    }
}