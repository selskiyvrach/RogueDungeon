using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyMoveConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyMoveState);
    }
}