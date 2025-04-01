using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerRaiseBlockConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerRaiseBlockMove);
    }
}