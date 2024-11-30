using System;
using RogueDungeon.Collisions;

namespace RogueDungeon.Player
{
    public enum DodgeState
    {
        None,
        Left,
        Right
    }

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