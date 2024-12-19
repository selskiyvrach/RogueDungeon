using RogueDungeon.Items.Bahaviour.WeaponWielder;

namespace RogueDungeon.Items.Weapons
{
    public interface IComboInfo 
    {
        AttackDirection[] AttackDirectionsInCombo { get; }
    }
}