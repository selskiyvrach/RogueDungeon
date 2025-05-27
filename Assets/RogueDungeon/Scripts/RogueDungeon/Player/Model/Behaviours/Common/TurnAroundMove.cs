using Common.Animations;
using Input;
using Levels;

namespace Player.Model.Behaviours.Common
{
    public class TurnAroundMove : TurnMove
    {
        private readonly Player _player;
        protected override InputKey RequiredKey => InputKey.TurnAround;
        protected override float RotationDegrees => 180;
        protected override float Duration => _player.Config.TurnAroundDuration;

        public TurnAroundMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(player, level, playerInput, animation, id) => 
            _player = player;
    }
}