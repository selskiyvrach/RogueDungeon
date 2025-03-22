using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Attacks
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