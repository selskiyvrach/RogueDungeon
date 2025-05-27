using Common.Animations;
using Input;
using Levels;

namespace Player.Model.Behaviours.Common
{
    public class TurnRightMove : TurnMove
    {
        private readonly Player _player;
        protected override InputKey RequiredKey => InputKey.TurnRight;
        protected override float RotationDegrees => -90;
        protected override float Duration => _player.Config.TurnDuration;

        public TurnRightMove(Player player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(player, level, playerInput, animation, id) => 
            _player = player;
    }
}