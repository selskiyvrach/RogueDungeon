using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerHoldBlockMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(ItemHoldBlockMove);
    }
}