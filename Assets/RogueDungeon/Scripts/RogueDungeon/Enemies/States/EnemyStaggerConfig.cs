using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyStaggerConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyStunState);
    }
}