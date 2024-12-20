namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public interface IWeaponInput
    {
        bool TryConsume(WeaponInputCommand weaponInputCommand);
        
    }
}