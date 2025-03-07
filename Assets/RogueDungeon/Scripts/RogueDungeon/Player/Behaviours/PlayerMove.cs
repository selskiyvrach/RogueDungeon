using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Behaviours
{
    public class PlayerMove : Move
    {
        private readonly PlayerMoveConfig _config;
        private readonly IPlayerInput _inputReader;

        protected PlayerMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput inputReader) : base(config, animation)
        {
            _config = config;
            _inputReader = inputReader;
        }

        public override void Enter()
        {
            if(_config.RequiredInputKey is not InputKey.None)
                _inputReader.ConsumeInput(_config.RequiredInputKey);
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_config.RequiredInputKey is InputKey.None || _inputReader.HasInput(_config.RequiredInputKey));
    }
}