using System;
using Common.Behaviours;

namespace RogueDungeon.Player.Behaviours.Items.WeaponWielder
{
    public class WeaponWielderExternalFacade : IBehaviourExternalFacade,ICanAttackSetter,
        IIsAttackInUncancellableAnimationPhaseGetter, IAttackHitEventObservable
    {
        private readonly IAttackHitEventObservable _hitEventObservable;
        private readonly ICanAttackSetter _canAttackSetter;
        private readonly IIsAttackInUncancellableAnimationPhaseGetter _isAttackInUncancellableAnimationPhaseGetter;

        event Action IAttackHitEventObservable.OnHit
        {
            add => _hitEventObservable.OnHit += value;
            remove => _hitEventObservable.OnHit -= value;
        }
        bool ICanAttackSetter.CanAttack
        {
            set => _canAttackSetter.CanAttack = value;
        }
        bool IIsAttackInUncancellableAnimationPhaseGetter.IsAttackInUncancellableAnimationState => _isAttackInUncancellableAnimationPhaseGetter.IsAttackInUncancellableAnimationState;

        public WeaponWielderExternalFacade(ICanAttackSetter canAttackSetter, IIsAttackInUncancellableAnimationPhaseGetter isAttackInUncancellableAnimationPhaseGetter, IAttackHitEventObservable hitEventObservable)
        {
            _canAttackSetter = canAttackSetter;
            _isAttackInUncancellableAnimationPhaseGetter = isAttackInUncancellableAnimationPhaseGetter;
            _hitEventObservable = hitEventObservable;
        }
    }
}