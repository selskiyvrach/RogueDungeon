using System;

namespace RogueDungeon.Collisions
{
    [Flags]
    public enum Positions
    {
        None = 0,
        PlayerDefault = 1,
        PlayerDodgeLeft = 2,
        PlayerDodgeRight = 4,
        EnemyFrontCenter = 8,
        EnemyBackLeft = 16,
        EnemyBackRight = 32,
    }
}