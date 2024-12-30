using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Behaviours.Items.Unsheather;
using Zenject;

namespace RogueDungeon.Player
{
    public class Player : IInitializable
    {
        private readonly IIntendedCurrentItemSetter _itemSetter;
        private readonly WeaponConfig _weaponConfig;

        public Player(IIntendedCurrentItemSetter itemSetter, WeaponConfig weaponConfig)
        {
            _itemSetter = itemSetter;
            _weaponConfig = weaponConfig;
        }

        public void Initialize() => 
            _itemSetter.Item = _weaponConfig;
    }
}