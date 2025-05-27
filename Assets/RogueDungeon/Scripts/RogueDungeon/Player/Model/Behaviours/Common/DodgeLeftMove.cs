using Common.Animations;
using Input;

namespace Player.Model.Behaviours.Common
{
    public class DodgeLeftMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeLeft;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingLeft;
        public DodgeLeftMove(Player player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : 
            base(player, animation, playerInput, playerControlState, id)
        {
        }

    }
}