using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyBirthConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyBirthState);
    }
}