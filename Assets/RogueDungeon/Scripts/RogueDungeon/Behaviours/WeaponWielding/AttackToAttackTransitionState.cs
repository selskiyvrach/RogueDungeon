using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IDurations _durations;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IComboInfo _comboInfo;

        protected override float Duration => _durations.Get(WeaponWielding.Duration.AttackAttackTransition);

        public AttackToAttackTransitionState(IDurations durations, IControlState controlState, IComboCounter comboCounter, IComboInfo comboInfo)
        {
            _durations = durations;
            _controlState = controlState;
            _comboCounter = comboCounter;
            _comboInfo = comboInfo;
        }

        public override void Enter()
        {
            base.Enter();
            _comboCounter.AttackIndex = ++_comboCounter.AttackIndex % _comboInfo.Directions.Length;
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