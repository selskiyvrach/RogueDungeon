using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyIdleConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyIdleState);
    }
}