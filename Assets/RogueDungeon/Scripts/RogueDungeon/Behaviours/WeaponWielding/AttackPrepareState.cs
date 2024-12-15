using Common.Animations;
using Common.Fsm;
using Common.Parameters;
using RogueDungeon.Characters;
using RogueDungeon.Fsm;
using RogueDungeon.Parameters;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackPrepareState : TiedToAnimationState
    {
        private readonly IParameters _durations;
        private readonly IControlState _controlState;
        private readonly IComboCounter _comboCounter;

        protected override AnimationData Animation => new(AnimationNames.ATTACK_PREPARE_TO_BOTTOM_LEFT, _durations.Get(ParameterKeys.ATTACK_IDLE_TRANSITION_DURATION));

        public AttackPrepareState(IAnimator animator, IParameters durations, IControlState controlState, IComboCounter comboCounter) : base(animator)
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
            if(_controlState.CanAttack())
                stateChanger.To<AttackExecutionState>();
            else
                stateChanger.To<AttackFinishState>();
        }
    }
}