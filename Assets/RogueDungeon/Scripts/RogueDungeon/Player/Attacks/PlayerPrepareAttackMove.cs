using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerPrepareAttackMove : PlayerAttackBaseMove
    {
        protected PlayerPrepareAttackMove(PlayerPrepareAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator mediator) : base(config, animation, playerInput, mediator)
        {
        }
    }
}