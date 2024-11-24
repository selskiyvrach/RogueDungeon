using System;

namespace RogueDungeon.WFC
{
    [Flags]
    public enum Edge
    {
        None = 0,
        Up = 1, 
        Right = 2, 
        Down = 4, 
        Left = 8,
    }
}