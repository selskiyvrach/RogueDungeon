using RogueDungeon.Items.Behaviours.Common;
using RogueDungeon.Items.Data.Weapons;

namespace RogueDungeon.Items.Behaviours.WeaponWielder
{
    public class WeaponWielderBehaviourContext : IComboCounter, IComboInfo, IWeaponControlState
    {
        private readonly ICurrentItemGetter _itemGetter;
        int IComboCounter.AttackIndex { get; set; }
        bool IWeaponControlState.IsAttackInHardPhase { get; set; }
        bool IWeaponControlState.CanAttack() => 
            true;

        AttackDirection[] IComboInfo.AttackDirectionsInCombo => ((IWeaponInfo)_itemGetter.Item).AttackDirectionsInCombo;
        public WeaponWielderBehaviourContext(ICurrentItemGetter itemGetter) => 
            _itemGetter = itemGetter;
    }
}