using Libs.Animations;
using Libs.Movesets;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class BirthMove : Move
    {
        private readonly PlayerModel _player;
        protected override float Duration => _player.Config.BirthAnimationDuration;

        public BirthMove(string id, IAnimation animation, PlayerModel player) : base(id, animation) => 
            _player = player;
    }
}