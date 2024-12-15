using RogueDungeon.Behaviours.WeaponWielding;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly WeaponBehaviour _weaponBehaviour;
        private readonly WeaponConfig _weaponConfig;

        public Player(WeaponBehaviour weaponBehaviour, WeaponConfig weaponConfig)
        {
            _weaponBehaviour = weaponBehaviour;
            _weaponConfig = weaponConfig;
        }

        public void Initialize()
        {
            _weaponBehaviour.WeaponInfo = _weaponConfig;
            _weaponBehaviour.Enable();
        }
    }
}