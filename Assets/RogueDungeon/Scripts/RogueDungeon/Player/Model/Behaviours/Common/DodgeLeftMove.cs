using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class DodgeLeftMove : DodgeMove
    {
        protected override InputKey RequiredKey => InputKey.DodgeLeft;
        protected override PlayerDodgeState DodgeState => PlayerDodgeState.DodgingLeft;
        public DodgeLeftMove(Player player, IAnimation animation, IPlayerInput playerInput, PlayerControlStateMediator playerControlState) : 
            base(player, animation, playerInput, playerControlState, Names.DODGE_LEFT)
        {
        }

    }
}