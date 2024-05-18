using System;

namespace RogueDungeon.Input
{
    [Flags]
    public enum Mode
    {
        None = 0,
        Combat = 1,
        Crossroad = 2,
        Exploration = 4,
        UI = 8,
    }
}