using Common.Animations;
using Common.Parameters;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeLeftState : DodgeExecutionState
    {
        protected override DodgeState DodgeState => DodgeState.DodgingLeft;

        public DodgeLeftState(IAnimator animator, IParameter<IDodgeDuration> duration, IDodgeStateSetter stateSetter) : base(animator, duration, stateSetter)
        {
        }
    }
}