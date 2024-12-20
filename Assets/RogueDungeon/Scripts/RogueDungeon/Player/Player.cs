using RogueDungeon.Items.Behaviours.Common;
using RogueDungeon.Items.Behaviours.Unsheather;
using RogueDungeon.Items.Behaviours.WeaponWielder;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Player
{
    public class Player
    {
        private readonly IIntendedItemSetter _itemSetter;
        private readonly WeaponWielderBehaviour _weaponBehaviour;
        private readonly UnsheatherBehaviour _unsheatherBehaviour;
        private readonly WeaponConfig _weaponConfig;

        public Player(IIntendedItemSetter itemSetter, WeaponWielderBehaviour weaponBehaviour, WeaponConfig weaponConfig, UnsheatherBehaviour unsheatherBehaviour)
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