using System;
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
        IComboInfo, 
        IAttackHitEventObservable,
        IAttackHitEventHandler
    {
        private readonly ICurrentItemGetter _currentItemGetter;
        public bool CanAttack { get; set; } = true;
        public bool IsAttackInUncancellableAnimationState { get; set; }
        public int AttackIndex { get; set; }
        public AttackDirection[] AttackDirectionsInCombo => (_currentItemGetter.Item as IWeaponInfo)?.AttackDirectionsInCombo;

        public WeaponWielderInternalFacade(ICurrentItemGetter currentItemGetter) => 
            _currentItemGetter = currentItemGetter;

        private event Action _onHit;
        event Action IAttackHitEventObservable.OnHit
        {
            add => _onHit += value;
            remove => _onHit -= value;
        }
        void IAttackHitEventHandler.HandleHit() => 
            _onHit?.Invoke();
    }
}