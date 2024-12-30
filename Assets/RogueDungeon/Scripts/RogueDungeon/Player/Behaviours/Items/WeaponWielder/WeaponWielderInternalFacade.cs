using Common.Behaviours;
using RogueDungeon.Items.Data.Weapons;
using RogueDungeon.Player.Behaviours.Items.Unsheather;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderInternalFacade : IBehaviourInternalFacade, 
        ICanAttackGetter, 
        ICanAttackSetter,
        IIsAttackInUncancellableAnimationPhaseSetter,
        IIsAttackInUncancellableAnimationPhaseGetter,
        IComboCounter,
        IComboInfo
    {
        private readonly ICurrentItemGetter _currentItemGetter;
        public bool CanAttack { get; set; } = true;
        public bool IsAttackInUncancellableAnimationState { get; set; }
        public int AttackIndex { get; set; }
        public AttackDirection[] AttackDirectionsInCombo => (_currentItemGetter.Item as IWeaponInfo)?.AttackDirectionsInCombo;

        public WeaponWielderInternalFacade(ICurrentItemGetter currentItemGetter) => 
            _currentItemGetter = currentItemGetter;
    }
}