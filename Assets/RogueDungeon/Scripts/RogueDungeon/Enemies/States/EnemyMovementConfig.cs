using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyMovementConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyMovementState);
    }
}