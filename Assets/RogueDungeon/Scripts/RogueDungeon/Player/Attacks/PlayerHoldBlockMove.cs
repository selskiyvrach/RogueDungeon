using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player;
using RogueDungeon.Player.Behaviours;

namespace RogueDungeon.Weapons
{
    public class PlayerHoldBlockMove : PlayerMove
    {
        private readonly IPlayerInput _playerInput;
        private readonly PlayerBlockerHandler _blocker;
        
        protected PlayerHoldBlockMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerBlockerHandler blocker) : base(config, animation, playerInput)
        {
            _playerInput = playerInput;
            _blocker = blocker;
        }

        public override void Enter()
        {
            base.Enter();
            _blocker.IsBlocking = true;
        }

        public override void Exit()
        {
            base.Exit();
            _blocker.IsBlocking = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerInput.HasInput(InputKey.Block);
    }
}