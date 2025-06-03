using Libs.Animations;
using Libs.Movesets;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class BirthMove : Move
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.BirthAnimationDuration;

        public BirthMove(string id, IAnimation animation, Player player) : base(id, animation) => 
            _player = player;
    }
}