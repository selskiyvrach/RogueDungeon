using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeMove : PlayerMove
    {
        private readonly IDodger _dodger;
        private readonly DodgeMoveConfig _config;

        public DodgeMove(IDodger dodger, DodgeMoveConfig config, IAnimation animation, IPlayerInput inputReader) : base(config, animation, inputReader)
        {
            _dodger = dodger;
            _config = config;
        }

        public override void Enter()
        {
            base.Enter();
            _dodger.DodgeState = _config.DodgeState;
        }
    }
}