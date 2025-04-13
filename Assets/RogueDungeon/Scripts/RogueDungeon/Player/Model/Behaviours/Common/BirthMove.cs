using Common.Animations;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class BirthMove : PlayerMove
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.BirthAnimationDuration;

        public BirthMove(string id, IAnimation animation, Player player) : base(id, animation) => 
            _player = player;
    }
}