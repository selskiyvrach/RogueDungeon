using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class IdleMove : PlayerMove
    {
        private readonly PlayerModel _player;
        protected override float Duration => _player.Config.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public IdleMove(PlayerModel player, IAnimation animation, string id) : base(id, animation) => 
            _player = player;

        public override void Tick(float timeDelta)
        {
            if(!_player.IsHoldingBreath)
                base.Tick(timeDelta);
        }
    }
}