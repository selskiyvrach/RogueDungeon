using System;

namespace RogueDungeon.Player.Behaviours.Common
{
    public class DeathMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(DeathMove);
    }
}