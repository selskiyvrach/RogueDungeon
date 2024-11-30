using System;
using Common.Events;
using Common.Properties;
using RogueDungeon.Animations;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;

namespace RogueDungeon.Player
{
    public class ControlState
    {
        private readonly IReadOnlyProperty<AttackState> _attackState;
        private readonly IReadOnlyProperty<DodgeState> _dodgeState;

        public ControlState(IReadOnlyProperty<AttackState> attackState, IReadOnlyProperty<DodgeState> dodgeState)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
        }

        private bool NotDodging => _dodgeState.Value == DodgeState.None;
        private bool NotExecutingAttack => _attackState.Value != AttackState.Executing;
        public bool CanStartHardHandsAnim() => NotDodging;
        public bool CanStartSoftHandsAnim() => NotDodging;
        public bool CanStartHardMovementAnim() => NotExecutingAttack;
    }

    public class Player : IGameEntity, IDodger
    {
        private readonly IEventBus<IAnimationEvent> _animationEvents;
        private IDisposable _sub;

        public Positions Position => _dodgeState.Value.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeState.Value;

        private readonly Property<AttackState> _attackState;
        private readonly Property<DodgeState> _dodgeState;

        public Player(Property<AttackState> attackState, Property<DodgeState> dodgeState)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}