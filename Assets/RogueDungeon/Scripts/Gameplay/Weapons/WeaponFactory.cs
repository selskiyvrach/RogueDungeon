using Zenject;

namespace RogueDungeon.Gameplay.Weapons
{
    public class WeaponFactory : IFactory<WeaponConfig, Weapon>
    {
        private readonly WeaponBehaviourFactory _weaponBehaviourFactory;

        public WeaponFactory(WeaponBehaviourFactory weaponBehaviourFactory) => 
            _weaponBehaviourFactory = weaponBehaviourFactory;

        public Weapon Create(WeaponConfig config) => 
            new(config, _weaponBehaviourFactory.Create(config));
    }
}