using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyDeathConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyDeathState);
    }
}