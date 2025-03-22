using System;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class DeathMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(DeathMove);
    }
}