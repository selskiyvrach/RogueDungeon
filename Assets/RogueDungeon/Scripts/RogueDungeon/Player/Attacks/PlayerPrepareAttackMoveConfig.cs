using System;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerPrepareAttackMoveConfig : PlayerAttackBaseMoveConfig
    {
        public override Type MoveType => typeof(PlayerPrepareAttackMove);
    }
}