using System;

namespace RogueDungeon.Characters
{
    [Flags]
    public enum Position
    {
        Player = 1,
        Frontline = 2,
        BacklineLeft = 4,
        BacklineRight = 8,
    }
}