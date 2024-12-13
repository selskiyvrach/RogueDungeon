using Common.Animations;
using Common.Fsm;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    internal class AttackPrepareState : TiedToAnimationState
    {
        private readonly IDurations _durations;
        private readonly IControlState _controlState;
        
        protected override AnimationData Animation => new(AnimationNames.ATTACK_FINISH_FROM_BOTTOM_RIGHT, _durations.Get(WeaponWielding.Duration.AttackIdleTransition));

        public AttackPrepareState(IAnimator animator, IDurations durations) : base(animator) => 
            _durations = durations;

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