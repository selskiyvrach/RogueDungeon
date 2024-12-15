using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public class WeaponFacade : IWeaponInfo
    {
        private readonly WeaponConfig _config;

        AttackDirection[] IComboInfo.AttackDirectionsInCombo => _config.AttackDirectionsInCombo;
        float IItemInfo.Weight => _config.Weight;
        public Sprite Sprite => _config.Sprite;
        float IDamageInfo.Damage => _config.Damage;
        DamageType IDamageInfo.Type => _config.Type;
        public WeaponBehaviour Behaviour { get; }
        public ItemWorldView ItemWorldView { get; }

        public WeaponFacade(WeaponBehaviour behaviour, ItemWorldView itemWorldView)
        {
            Behaviour = behaviour;
            ItemWorldView = itemWorldView;
        }
    }
}