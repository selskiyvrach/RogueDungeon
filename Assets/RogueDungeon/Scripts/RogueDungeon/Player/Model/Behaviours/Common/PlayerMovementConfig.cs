using System;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class PlayerMovementConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(MoveForwardMove);
    }
}