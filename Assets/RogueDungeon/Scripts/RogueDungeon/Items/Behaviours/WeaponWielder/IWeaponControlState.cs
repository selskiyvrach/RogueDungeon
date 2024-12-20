namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public interface IWeaponControlState
    {
        bool CanAttack();
        bool IsAttackInHardPhase { get; set; }
    }
}