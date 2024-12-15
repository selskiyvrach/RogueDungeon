using System;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    [Flags]
    public enum DamageType
    {
        None = 0,
        Slash = 1,
        Blunt = 2,
        Pierce = 4,
        Mixed = Slash | Blunt | Pierce
    }
}