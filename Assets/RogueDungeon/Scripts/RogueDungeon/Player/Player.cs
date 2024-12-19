using RogueDungeon.Items.Bahaviour.Common;
using RogueDungeon.Items.Bahaviour.Unsheather;
using RogueDungeon.Items.Bahaviour.WeaponWielder;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly IIntendedItemSetter _itemSetter;
        private readonly WeaponBehaviour _weaponBehaviour;
        private readonly UnsheatherBehaviour _unsheatherBehaviour;
        private readonly WeaponConfig _weaponConfig;

        public Player(IIntendedItemSetter itemSetter, WeaponBehaviour weaponBehaviour, WeaponConfig weaponConfig, UnsheatherBehaviour unsheatherBehaviour)
        {
            _itemSetter = itemSetter;
            _weaponBehaviour = weaponBehaviour;
            _weaponConfig = weaponConfig;
            _unsheatherBehaviour = unsheatherBehaviour;
        }

        public void Initialize()
        {
            _itemSetter.Item = _weaponConfig;
            _unsheatherBehaviour.Enable();
        }
    }
}