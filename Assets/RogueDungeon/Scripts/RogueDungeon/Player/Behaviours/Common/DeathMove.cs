using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Behaviours.Common
{
    public class DeathMove : PlayerMove
    {
        private readonly Player _player;

        protected DeathMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput, Player player) : base(config, animation, playerInput) => 
            _player = player;

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if(IsFinished && !_player.IsReadyToBeDisposed)
                _player.IsReadyToBeDisposed = true;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_player.IsAlive;
    }
}