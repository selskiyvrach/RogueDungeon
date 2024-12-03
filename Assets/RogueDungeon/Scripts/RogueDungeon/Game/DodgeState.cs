using System;
using RogueDungeon.Behaviours;
using RogueDungeon.Behaviours.DodgeBehaviour;
using RogueDungeon.Collisions;

namespace RogueDungeon.Player
{


    public static class DodgeStateExtensions
    {
        public static Positions ToPlayerPosition(this DodgeState state) => state switch
        {
            DodgeState.None => Positions.PlayerDefault,
            DodgeState.Left => Positions.PlayerDodgeLeft,
            DodgeState.Right => Positions.PlayerDodgeRight,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}