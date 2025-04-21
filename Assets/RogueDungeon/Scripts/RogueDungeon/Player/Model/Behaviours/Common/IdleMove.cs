using Common.Animations;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class IdleMove : PlayerMove
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.IdleAnimationDuration;
        protected override bool IsLooping => true;

        public IdleMove(Player player, IAnimation animation, string id) : base(id, animation) => 
            _player = player;

        public override void Tick(float timeDelta)
        {
            if(!_player.IsHoldingBreath)
                base.Tick(timeDelta);
        }
    }
}