using System;

namespace RogueDungeon.Characters
{
    [Flags]
    public enum DodgeState
    {
        NotDodging = 0,
        DodgingRight = 1,
        DodgingLeft = 2,
    }
}