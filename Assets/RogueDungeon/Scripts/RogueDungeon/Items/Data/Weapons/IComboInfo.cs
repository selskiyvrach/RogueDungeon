using RogueDungeon.Items.Handling.WeaponWielder;

namespace RogueDungeon.Items.Weapons
{
    public interface IComboInfo 
    {
        AttackDirection[] AttackDirectionsInCombo { get; }
    }
}