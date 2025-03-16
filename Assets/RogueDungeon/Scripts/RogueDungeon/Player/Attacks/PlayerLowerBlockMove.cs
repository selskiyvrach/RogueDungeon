using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerLowerBlockMove : PlayerMove
    {
        private readonly IPlayerInput _playerInput;
        
        protected PlayerLowerBlockMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation, playerInput) => 
            _playerInput = playerInput;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_playerInput.HasInput(InputKey.Block);
    }
}