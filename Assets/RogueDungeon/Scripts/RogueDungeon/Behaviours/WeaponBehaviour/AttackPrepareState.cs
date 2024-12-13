namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal class AttackPrepareState : TiedToAnimationState
    {
        private readonly IControlState _controlState;
        
        protected override Animation Animation => Animation.PrepareAttackToBottomLeft;

        public AttackPrepareState(IAnimator animator, IDurations durations) : 
            base(animator, durations, WeaponBehaviour.Duration.AttackIdleTransition)
        {
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