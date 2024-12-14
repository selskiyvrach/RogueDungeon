using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackPrepareState : TiedToAnimationState
    {
        private readonly IDurations _durations;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;

        protected override AnimationData Animation => new(AnimationNames.ATTACK_PREPARE_TO_BOTTOM_LEFT, _durations.Get(WeaponWielding.Duration.AttackIdleTransition));

        public AttackPrepareState(IAnimator animator, IDurations durations, IControlState controlState, IComboCounter comboCounter) : base(animator)
        {
            _durations = durations;
            _controlState = controlState;
            _comboCounter = comboCounter;
        }

        public override void Enter()
        {
            _comboCounter.AttackIndex++;
            base.Enter();
        }

        public override void CheckTransitions(IStateChanger stateChanger)
        {
            if(!IsTimerOff)
                return;
            if(_controlState.Is(AbleTo.TransitionToAttackExecutionState))
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}