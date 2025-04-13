using Common.Animations;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class IdleMove : PlayerMove
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.IdleAnimationDuration; 

        public IdleMove(Player player, IAnimation animation) : base(Names.IDLE, animation) => 
            _player = player;
    }
}