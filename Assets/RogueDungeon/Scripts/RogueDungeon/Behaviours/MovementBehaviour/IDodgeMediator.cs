using System;

namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public interface IDodgeMediator
    {
        bool CanStartDodge();
        DodgeState DodgeState { get; set; }
    }
    
    public class DummyDodgeMediator : IDodgeMediator
    {
        private readonly Func<bool> _canStartChecker;
        private readonly Action<DodgeState> _dodgeStateSetter;
        private readonly Func<DodgeState> _dodgeStateGetter;

        public DodgeState DodgeState
        {
            get => _dodgeStateGetter?.Invoke() ?? DodgeState.None;
            set => _dodgeStateSetter?.Invoke(value);
        }

        public DummyDodgeMediator(Func<bool> canStartChecker, Action<DodgeState> dodgeStateSetter, Func<DodgeState> dodgeStateGetter)
        {
            _canStartChecker = canStartChecker;
            _dodgeStateSetter = dodgeStateSetter;
            _dodgeStateGetter = dodgeStateGetter;
        }

        public bool CanStartDodge() => 
            _canStartChecker.Invoke();
    }
}