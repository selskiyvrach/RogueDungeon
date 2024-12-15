using RogueDungeon.Behaviours.WeaponWielding;

namespace RogueDungeon.Items.Weapons
{
    public interface IComboInfo 
    {
        AttackDirection[] AttackDirectionsInCombo { get; }
    }
}