using RogueDungeon.Items.Bahaviour.Common;
using RogueDungeon.Items.Weapons;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    public class WeaponBehaviourContext : IComboCounter, IComboInfo
    {
        private readonly ICurrentItemGetter _itemGetter;
        int IComboCounter.AttackIndex { get; set; }
        AttackDirection[] IComboInfo.AttackDirectionsInCombo => ((IWeaponInfo)_itemGetter.Item).AttackDirectionsInCombo;

        public WeaponBehaviourContext(ICurrentItemGetter itemGetter) => 
            _itemGetter = itemGetter;
    }
}