using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class DodgeMove : PlayerMove
    {
        private readonly PlayerControlStateMediator _playerControlState;
        private readonly Player _player;
        private readonly DodgeMoveConfig _config;

        public DodgeMove(Player player, DodgeMoveConfig config, IAnimation animation, IPlayerInput playerInput, PlayerControlStateMediator playerControlState) : base(config, animation, playerInput)
        {
            _player = player;
            _config = config;
            _playerControlState = playerControlState;
        }

        public override void Enter()
        {
            base.Enter();
            _player.Stamina.AddDelta(- _config.StaminaCost);
            _player.DodgeState = _config.DodgeState;
            _playerControlState.IsDodging = _config.DodgeState != PlayerDodgeState.None;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlState.IsDodging = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerControlState.CanDodge && _player.Stamina.Current > 1;
    }
}