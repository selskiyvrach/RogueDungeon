using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;

namespace RogueDungeon.Weapons
{
    public class PlayerFinishAttackMove : PlayerAttackBaseMove
    {
        private readonly PlayerControlStateMediator _controlStateMediator;
        
        protected PlayerFinishAttackMove(PlayerFinishAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator mediator, PlayerControlStateMediator controlStateMediator) : base(config, animation, playerInput, mediator)
        {
            _controlStateMediator = controlStateMediator;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() || !_controlStateMediator.CanAttack;
    }
}