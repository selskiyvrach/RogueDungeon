using System;

namespace RogueDungeon.Enemies.MoveSet
{
    public class EnemyIdleConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyIdleState);
    }
}