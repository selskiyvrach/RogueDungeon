using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Movement
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