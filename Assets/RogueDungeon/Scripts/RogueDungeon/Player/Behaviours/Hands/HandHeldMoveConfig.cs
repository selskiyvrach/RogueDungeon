using System;
using Common.MoveSets;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class HandHeldMoveConfig : MoveConfig
    {
        public override Type MoveType => Id switch {
            "Idle" => typeof(HandHeldIdle),
            "Unsheath" => typeof(UnsheathMove),
            "Sheath" => typeof(SheathMove),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}