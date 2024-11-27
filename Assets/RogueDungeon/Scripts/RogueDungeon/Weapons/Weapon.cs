using Common.Registries;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;

namespace RogueDungeon.Weapons
{
    public interface IWeapon 
    {
    }

    public class Weapon : IWeapon
    {
        private readonly WeaponConfig _config;
        public WeaponBehaviour WeaponBehaviour { get; private set; }
        
        public IRegistry<Parameter> Parameters { get; }
        public IRegistry<Property> Properties { get; }


        public Weapon(WeaponConfig config, WeaponBehaviour weaponBehaviour)
        {
            _config = config;
            WeaponBehaviour = weaponBehaviour;
        }
    }
}