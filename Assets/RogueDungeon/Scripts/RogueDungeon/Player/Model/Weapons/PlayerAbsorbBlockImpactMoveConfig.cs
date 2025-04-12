using System;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerAbsorbBlockImpactMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerAbsorbBlockImpactMove);
    }
}