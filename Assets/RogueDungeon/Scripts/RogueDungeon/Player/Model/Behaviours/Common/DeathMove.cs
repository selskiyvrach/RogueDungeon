using Common.Animations;

namespace Player.Model.Behaviours.Common
{
    public class DeathMove : PlayerMove
    {
        private readonly PlayerModel _player;
        protected override float Duration => _player.Config.DeathAnimationDuration;

        protected DeathMove(IAnimation animation, PlayerModel player,string id) : base(id, animation) => 
            _player = player;

        public override void Enter()
        {
            base.Enter();
            _player.Hands.Disable(force: true);
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if (IsFinished && !_player.IsReadyToBeDisposed) 
                _player.IsReadyToBeDisposed = true;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_player.IsAlive;
    }
}