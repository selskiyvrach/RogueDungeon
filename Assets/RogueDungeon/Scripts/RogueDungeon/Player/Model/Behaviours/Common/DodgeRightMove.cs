using Common.Animations;
using Input;

namespace Player.Model.Behaviours.Common
{
    public class DodgeRightMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeRight;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingRight;
        public DodgeRightMove(Player player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : 
            base(player, animation, playerInput, playerControlState, id)
        {
        }

    }
}