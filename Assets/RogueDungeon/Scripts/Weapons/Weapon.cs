namespace RogueDungeon.Weapons
{
    public class Weapon
    {
        private readonly WeaponConfig _config;
        public WeaponBehaviour WeaponBehaviour { get; private set; }
        public Weapon(WeaponConfig config, WeaponBehaviour weaponBehaviour)
        {
            _config = config;
            WeaponBehaviour = weaponBehaviour;
        }
    }
}