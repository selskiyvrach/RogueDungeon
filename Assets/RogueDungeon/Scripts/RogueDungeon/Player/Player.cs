using RogueDungeon.Items.Handling.Unsheather;
using RogueDungeon.Items.Handling.WeaponWielder;
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
        }
    }
}