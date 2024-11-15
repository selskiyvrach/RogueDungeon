using RogueDungeon.Services.AssetManagement;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class WeaponFactory : IFactory<WeaponConfig, Weapon>
    {
        private readonly WeaponBehaviourFactory _weaponBehaviourFactory;
        private readonly IAssetProvider<WeaponConfig> _configProvider;

        public WeaponFactory(WeaponBehaviourFactory weaponBehaviourFactory, IAssetProvider<WeaponConfig> configProvider)
        {
            _weaponBehaviourFactory = weaponBehaviourFactory;
            _configProvider = configProvider;
        }

        public Weapon Create(WeaponConfig config) => 
            new(config, _weaponBehaviourFactory.Create(config));
    }
}