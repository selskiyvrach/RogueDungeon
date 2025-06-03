using Game.Libs.Input;
using Game.Libs.Movesets;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public abstract class DodgeMove : PlayerInputMove
    {
        private readonly PlayerControlStateMediator _playerControlState;
        private readonly Player _player;
        protected abstract DodgeState DodgeState { get; }
        protected sealed override RequiredState State => RequiredState.Down;
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
        }

        public override void Exit()
        {
            _player.DodgeState = DodgeState.None;
            base.Exit();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerControlState.CanDodge && _player.Stamina.Current >= _player.DodgeStaminaCost;
    }
}