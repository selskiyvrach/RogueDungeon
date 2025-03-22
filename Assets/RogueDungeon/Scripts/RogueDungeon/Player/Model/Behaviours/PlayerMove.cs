using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours
{
    public class PlayerMove : Move
    {
        private readonly PlayerMoveConfig _config;
        private readonly IPlayerInput _playerInput;

        protected PlayerMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation)
        {
            _config = config;
            _playerInput = playerInput;
        }

        public override void Enter()
        {
            if(_config.RequiredInputKey is not InputKey.None)
                _playerInput.ConsumeInput(_config.RequiredInputKey);
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_config.RequiredInputKey is InputKey.None || _playerInput.HasInput(_config.RequiredInputKey));
    }
}