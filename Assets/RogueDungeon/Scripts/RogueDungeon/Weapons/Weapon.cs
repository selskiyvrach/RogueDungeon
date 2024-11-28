namespace RogueDungeon.Weapons
{
    public interface IWeapon 
    {
    }

    [CreateScriptableInstaller(bindAs: typeof(IWeapon), serializedFields: new []{typeof(int), typeof(WeaponConfig), typeof(WeaponConfig)})]
    public class Weapon : IWeapon
    {
        
    }
}