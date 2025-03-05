using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyBirthConfig : EnemyMoveConfig
    {
        public override Type MoveType => typeof(EnemyBirthMove);
    }
}