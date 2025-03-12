using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyDeathConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyDeathState);
    }
}