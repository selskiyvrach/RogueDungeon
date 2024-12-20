using RogueDungeon.Items.Behaviours.Common;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class WeaponWielderBehaviourContext : IComboCounter, IComboInfo
    {
        private readonly ICurrentItemGetter _itemGetter;
        int IComboCounter.AttackIndex { get; set; }
        AttackDirection[] IComboInfo.AttackDirectionsInCombo => ((IWeaponInfo)_itemGetter.Item).AttackDirectionsInCombo;

        public WeaponWielderBehaviourContext(ICurrentItemGetter itemGetter) => 
            _itemGetter = itemGetter;
    }
}