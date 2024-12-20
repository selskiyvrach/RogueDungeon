using Common.Animations;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeLeftState : DodgeExecutionState
    {
        protected override DodgeState DodgeState => DodgeState.DodgingLeft;

        public DodgeLeftState(IAnimator animator, IDodgeDuration duration, IDodgeStateSetter stateSetter) : base(animator, duration, stateSetter)
        {
        }
    }
}