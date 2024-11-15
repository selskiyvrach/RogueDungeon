using RogueDungeon.Weapons;
using Zenject;

namespace RogueDungeon.Player
{
    public class EquipmentManager
    {
        private readonly IFactory<WeaponConfig, Weapon> _weaponFactory;
        private readonly EquipmentConfig _config;

        public EquipmentManager(IFactory<WeaponConfig, Weapon> weaponFactory, EquipmentConfig config)
        {
            _weaponFactory = weaponFactory;
            _config = config;
        }
    }
}