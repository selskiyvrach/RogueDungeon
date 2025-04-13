using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class DodgeRightMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeRight;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingRight;
        public DodgeRightMove(Player player, IAnimation animation, IPlayerInput playerInput, PlayerControlStateMediator playerControlState) : 
            base(player, animation, playerInput, playerControlState, Names.DODGE_RIGHT)
        {
        }

    }
}