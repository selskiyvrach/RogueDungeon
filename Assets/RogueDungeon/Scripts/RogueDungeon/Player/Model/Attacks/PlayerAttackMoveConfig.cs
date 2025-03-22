using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        public override Type MoveType => typeof(PlayerAttackMove);
    }
}