using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IDurations _durations;
        private readonly IControlState _controlState;

        protected override float Duration => _durations.Get(WeaponWielding.Duration.AttackAttackTransition);

        public AttackToAttackTransitionState(IDurations durations, IControlState controlState)
        {
            _durations = durations;
            _controlState = controlState;
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if (_controlState.Is(AbleTo.TransitionToAttackExecutionState))
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}