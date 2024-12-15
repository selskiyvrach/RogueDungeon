using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Parameters;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackToAttackTransitionState : TimerState
    {
        private readonly IParameters _durations;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;
        private readonly IComboInfo _comboInfo;

        protected override float Duration => _durations.Get(ParameterKeys.ATTACK_TO_ATTACK_TRANSITION_DURATION);

        public AttackToAttackTransitionState(IParameters durations, IControlState controlState, IComboCounter comboCounter, IComboInfo comboInfo)
        {
            _durations = durations;
            _controlState = controlState;
            _comboCounter = comboCounter;
            _comboInfo = comboInfo;
        }

        public override void Enter()
        {
            _comboCounter.AttackIndex = ++_comboCounter.AttackIndex % _comboInfo.Directions.Length;
            base.Enter();
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