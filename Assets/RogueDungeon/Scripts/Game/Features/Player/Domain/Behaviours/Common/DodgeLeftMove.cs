using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class DodgeLeftMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeLeft;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingLeft;
        public DodgeLeftMove(PlayerModel player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : 
            base(player, animation, playerInput, playerControlState, id)
        {
        }

    }
}