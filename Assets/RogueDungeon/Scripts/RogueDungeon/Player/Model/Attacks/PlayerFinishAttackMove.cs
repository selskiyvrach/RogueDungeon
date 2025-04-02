using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerFinishAttackMove : PlayerMove
    {
        private readonly PlayerControlStateMediator _controlStateMediator;
        
        protected PlayerFinishAttackMove(PlayerFinishAttackMoveConfig config, IAnimation animation, IPlayerInput playerInput, 
            PlayerControlStateMediator controlStateMediator) : base(config, animation, playerInput) =>
            
            _controlStateMediator = controlStateMediator;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() || !_controlStateMediator.CanAttack;
    }
}