using System;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerLowerBlockConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerLowerBlockMove);
    }
}