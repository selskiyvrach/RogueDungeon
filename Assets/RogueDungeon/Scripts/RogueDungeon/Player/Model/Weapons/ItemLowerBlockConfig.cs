using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemLowerBlockConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(ItemLowerBlockMove);
    }
}