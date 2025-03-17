using System;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerFinishAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        public override Type MoveType => typeof(PlayerFinishAttackMove);
    }
}