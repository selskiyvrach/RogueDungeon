using Game.Features.Levels;
using Game.Features.Levels.Domain;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class TurnAroundMove : TurnMove
    {
        private readonly PlayerModel _player;
        protected override InputKey RequiredKey => InputKey.TurnAround;
        protected override float RotationDegrees => 180;
        protected override float Duration => _player.Config.TurnAroundDuration;

        public TurnAroundMove(PlayerModel player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(player, level, playerInput, animation, id) => 
            _player = player;
    }
}