using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerPrepareAttackMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(ItemPrepareAttackMove);
    }
}