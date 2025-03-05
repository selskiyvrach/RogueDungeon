using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyIdleConfig : EnemyMoveConfig
    {
        public override Type MoveType => typeof(EnemyIdleMove);
    }
}