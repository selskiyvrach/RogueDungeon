using Common.Animations;
using Common.Parameters;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeRightState : DodgeExecutionState
    {
        protected override DodgeState DodgeState => DodgeState.DodgingRight;

        public DodgeRightState(IAnimator animator, IParameter<IDodgeDuration> duration, IDodgeStateSetter stateSetter) : base(animator, duration, stateSetter)
        {
        }
    }
}