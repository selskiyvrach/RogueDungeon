using Common.Behaviours;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderInternalFacade : IBehaviourInternalFacade, 
        ICanAttackGetter, 
        ICanAttackSetter,
        IIsAttackInUncancellableAnimationPhaseSetter,
        IIsAttackInUncancellableAnimationPhaseGetter,
        IComboCounter
    {
        public bool CanAttack { get; set; }
        public bool IsAttackInUncancellableAnimationState { get; set; }
        public int AttackIndex { get; set; }
    }
}