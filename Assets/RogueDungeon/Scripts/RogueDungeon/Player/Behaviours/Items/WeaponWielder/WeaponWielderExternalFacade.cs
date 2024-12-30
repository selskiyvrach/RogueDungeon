using Common.Behaviours;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderExternalFacade : IBehaviourExternalFacade,ICanAttackSetter,
        IIsAttackInUncancellableAnimationPhaseGetter 
    {
        private readonly ICanAttackSetter _canAttackSetter;
        private readonly IIsAttackInUncancellableAnimationPhaseGetter _isAttackInUncancellableAnimationPhaseGetter;

        public bool CanAttack
        {
            set => _canAttackSetter.CanAttack = value;
        }
        public bool IsAttackInUncancellableAnimationState => _isAttackInUncancellableAnimationPhaseGetter.IsAttackInUncancellableAnimationState;

        public WeaponWielderExternalFacade(ICanAttackSetter canAttackSetter, IIsAttackInUncancellableAnimationPhaseGetter isAttackInUncancellableAnimationPhaseGetter)
        {
            _canAttackSetter = canAttackSetter;
            _isAttackInUncancellableAnimationPhaseGetter = isAttackInUncancellableAnimationPhaseGetter;
        }
    }
}