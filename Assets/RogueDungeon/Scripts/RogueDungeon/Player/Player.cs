using RogueDungeon.Items.Bahaviour.Unsheather;
using RogueDungeon.Items.Bahaviour.WeaponWielder;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly WeaponBehaviour _weapon;
        private readonly UnsheatherBehaviour _unsheatherBehaviour;
        private readonly WeaponConfig _weaponConfig;

        public Player(WeaponBehaviour weapon, WeaponConfig weaponConfig, UnsheatherBehaviour unsheatherBehaviour)
        {
            _weapon = weapon;
            _weaponConfig = weaponConfig;
            _unsheatherBehaviour = unsheatherBehaviour;
        }

        public void Initialize()
        {
        }
    }
}