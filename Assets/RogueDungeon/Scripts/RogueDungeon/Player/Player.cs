using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Behaviours.Items.Unsheather;
using Zenject;

namespace RogueDungeon.Player
{
    public class Player : IInitializable, ITickable
    {
        private readonly IIntendedCurrentItemSetter _itemSetter;
        private readonly WeaponConfig _weaponConfig;

        private bool _itemIsNull = true;

        public Player(IIntendedCurrentItemSetter itemSetter, WeaponConfig weaponConfig)
        {
            _itemSetter = itemSetter;
            _weaponConfig = weaponConfig;
        }

        public void Initialize() =>
            SwitchWeapon();

        public void Tick()
        {
            if (UnityEngine.Input.GetMouseButtonDown(1))
                SwitchWeapon();
        }

        private void SwitchWeapon()
        {
            _itemSetter.Item = _itemIsNull ? _weaponConfig : null;
            _itemIsNull = !_itemIsNull;
        }
    }
}