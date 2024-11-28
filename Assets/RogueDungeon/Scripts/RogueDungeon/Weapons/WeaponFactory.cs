using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponFactory : IFactory<WeaponConfig, IWeapon, Weapon>
    {
        private readonly WeaponBehaviourFactory _weaponBehaviourFactory;

        public WeaponFactory(WeaponBehaviourFactory weaponBehaviourFactory) => 
            _weaponBehaviourFactory = weaponBehaviourFactory;

        public Weapon Create(WeaponConfig config, IWeapon weapon)
        {
            return null; // new(config, _weaponBehaviourFactory.Create(config, weapon));
        }
    }
}