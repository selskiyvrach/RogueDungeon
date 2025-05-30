using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class DodgeRightMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeRight;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingRight;
        public DodgeRightMove(PlayerModel player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : 
            base(player, animation, playerInput, playerControlState, id)
        {
        }

    }
}