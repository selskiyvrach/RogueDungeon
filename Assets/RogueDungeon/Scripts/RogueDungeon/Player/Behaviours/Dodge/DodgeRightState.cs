using Common.Animations;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeRightState : DodgeExecutionState
    {
        protected override DodgeState DodgeState => DodgeState.DodgingRight;

        public DodgeRightState(IAnimator animator, IDodgeDuration duration, IDodgeStateSetter stateSetter) : base(animator, duration, stateSetter)
        {
        }
    }
}