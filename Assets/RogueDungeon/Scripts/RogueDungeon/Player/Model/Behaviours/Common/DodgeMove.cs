using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public abstract class DodgeMove : PlayerInputMove
    {
        private readonly PlayerControlStateMediator _playerControlState;
        private readonly Player _player;
        protected abstract PlayerDodgeState DodgeState { get; }
        protected override float Duration => _player.Config.DodgeDuration;

        protected DodgeMove(Player player, IAnimation animation, IPlayerInput playerInput,
            PlayerControlStateMediator playerControlState, string id) : base(id, animation, playerInput)
        {
            _player = player;
            _playerControlState = playerControlState;
        }

        public override void Enter()
        {
            base.Enter();
            _player.Stamina.AddDelta(- _player.DodgeStaminaCost);
            _player.DodgeState = DodgeState;
            _playerControlState.IsDodging = DodgeState != PlayerDodgeState.None;
        }

        public override void Exit()
        {
            base.Exit();
            _playerControlState.IsDodging = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerControlState.CanDodge && _player.Stamina.Current >= _player.DodgeStaminaCost;
    }
}