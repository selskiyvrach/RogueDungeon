using System;

namespace RogueDungeon.Player.Behaviours.Common
{
    public class PlayerMovementConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerMovementState);
    }
}