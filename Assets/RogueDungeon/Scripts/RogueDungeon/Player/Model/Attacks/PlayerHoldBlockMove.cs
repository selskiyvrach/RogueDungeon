using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;

namespace RogueDungeon.Player.Model.Attacks
{
    public class PlayerHoldBlockMove : PlayerMove
    {
        private readonly IPlayerInput _playerInput;
        private readonly Player _player;
        
        protected PlayerHoldBlockMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player) : base(config, animation, playerInput)
        {
            _playerInput = playerInput;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.BlockerHandler.IsBlocking = true;
        }

        public override void Exit()
        {
            base.Exit();
            _player.BlockerHandler.IsBlocking = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerInput.HasInput(InputKey.Block);
    }
}