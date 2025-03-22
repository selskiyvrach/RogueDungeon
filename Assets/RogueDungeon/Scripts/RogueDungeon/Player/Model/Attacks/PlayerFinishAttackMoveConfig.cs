using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerFinishAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        public override Type MoveType => typeof(PlayerFinishAttackMove);
    }
}