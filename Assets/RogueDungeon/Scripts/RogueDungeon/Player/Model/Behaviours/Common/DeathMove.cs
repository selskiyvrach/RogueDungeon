using Common.Animations;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class DeathMove : PlayerMove
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.DeathAnimationDuration;

        protected DeathMove(IAnimation animation, Player player,string id) : base(id, animation) => 
            _player = player;

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