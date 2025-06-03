using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class DodgeRightMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeRight;
        protected override DodgeState DodgeState => DodgeState.DodgingRight;
        public DodgeRightMove(Player player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : 
            base(player, animation, playerInput, playerControlState, id)
        {
        }

    }
}