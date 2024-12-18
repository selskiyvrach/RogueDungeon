using RogueDungeon.Behaviours.HandheldEquipmentBehaviour;
using RogueDungeon.Behaviours.WeaponWielding;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly WeaponBehaviour _weapon;
        private readonly HandheldItemBehaviour _handheldItemBehaviour;
        private readonly WeaponConfig _weaponConfig;

        public Player(WeaponBehaviour weapon, WeaponConfig weaponConfig, HandheldItemBehaviour handheldItemBehaviour)
        {
            _weapon = weapon;
            _weaponConfig = weaponConfig;
            _handheldItemBehaviour = handheldItemBehaviour;
        }

        public void Initialize()
        {
            EquipWeapon();
        }

        private void EquipWeapon()
        {
            _weapon.WeaponInfo = _weaponConfig;
            _weapon.Enable();
        }
    }
}