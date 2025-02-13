using Common.Animations;
using RogueDungeon.Characters.Input;

namespace RogueDungeon.MoveSets
{
    public class PlayerMove : Move
    {
        private readonly PlayerMoveConfig _config;
        private readonly ICharacterInput _inputReader;

        public PlayerMove(PlayerMoveConfig config, IAnimator animator, ICharacterInput inputReader) : base(config, animator)
        {
            _config = config;
            _inputReader = inputReader;
        }

        public override void Enter()
        {
            if(_config.RequiredInput is not Input.None)
                _inputReader.ConsumeInput(_config.RequiredInput);
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_config.RequiredInput is Input.None || _inputReader.HasInput(_config.RequiredInput));
    }
}