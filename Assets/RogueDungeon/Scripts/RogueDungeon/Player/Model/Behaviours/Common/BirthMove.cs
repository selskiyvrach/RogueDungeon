using Common.Animations;
using Common.MoveSets;

namespace Player.Model.Behaviours.Common
{
    public class BirthMove : Move
    {
        private readonly PlayerModel _player;
        protected override float Duration => _player.Config.BirthAnimationDuration;

        public BirthMove(string id, IAnimation animation, PlayerModel player) : base(id, animation) => 
            _player = player;
    }
}