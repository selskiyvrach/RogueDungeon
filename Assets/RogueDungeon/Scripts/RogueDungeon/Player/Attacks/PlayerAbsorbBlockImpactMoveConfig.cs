using System;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerAbsorbBlockImpactMoveConfig : PlayerMoveConfig
    {
        public override Type MoveType => typeof(PlayerAbsorbBlockImpactMove);
    }
}