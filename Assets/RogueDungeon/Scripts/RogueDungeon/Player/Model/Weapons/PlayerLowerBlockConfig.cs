using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerLowerBlockConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerLowerBlockMove);
    }
}