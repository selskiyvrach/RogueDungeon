using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemIdleMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(ItemIdleMove);
    }
}