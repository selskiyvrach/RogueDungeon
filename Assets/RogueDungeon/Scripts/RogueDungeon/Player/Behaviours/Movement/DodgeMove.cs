using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class DodgeMove : PlayerMove
    {
        private readonly IDodger _dodger;
        private readonly DodgeMoveConfig _config;

        public DodgeMove(IDodger dodger, DodgeMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation, playerInput)
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