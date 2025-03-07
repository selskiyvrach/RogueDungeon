using System;
using Common.MoveSets;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class HandHeldMoveConfig : MoveConfig
    {
        public override Type MoveType => Id switch {
            "idle" => typeof(HandHeldIdle),
            "unsheath" => typeof(UnsheathMove),
            "sheath" => typeof(SheathMove),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}