using System;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerMovementState);
    }
}