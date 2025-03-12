using System;

namespace RogueDungeon.Enemies.States
{
    public class EnemyBirthConfig : EnemyStateConfig
    {
        public override Type StateType => typeof(EnemyBirthState);
    }
}