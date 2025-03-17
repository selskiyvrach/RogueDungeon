using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class DodgeMove : PlayerMove
    {
        private readonly PlayerControlStateMediator _playerControlState;
        private readonly IDodger _dodger;
        private readonly DodgeMoveConfig _config;

        public DodgeMove(IDodger dodger, DodgeMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerControlStateMediator playerControlState) : base(config, animation, playerInput)
        {
            _dodger = dodger;
            _config = config;
            _playerControlState = playerControlState;
        }

        public override void Enter()
        {
            base.Enter();
            _dodger.DodgeState = _config.DodgeState;
            _playerControlState.IsDodging = _config.DodgeState != PlayerDodgeState.None;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlState.IsDodging = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerControlState.CanDodge;
    }
}