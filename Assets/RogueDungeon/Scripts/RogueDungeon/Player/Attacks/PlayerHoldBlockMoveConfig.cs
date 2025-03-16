using System;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerHoldBlockMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerHoldBlockMove);
    }
}