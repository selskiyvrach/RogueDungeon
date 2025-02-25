using System;
using Common.MoveSets;

namespace RogueDungeon.Enemies
{
    public class EnemyMoveConfig : MoveConfig
    {
        public override Type MoveType => typeof(EnemyMove);
    }
}