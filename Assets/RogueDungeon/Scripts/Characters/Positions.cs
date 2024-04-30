using System;

namespace RogueDungeon.Characters
{
    [Flags]
    public enum Positions
    {
        Player = 1,
        Frontline = 2,
        BacklineLeft = 4,
        BacklineRight = 8,
    }
}