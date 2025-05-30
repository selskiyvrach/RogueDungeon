using Game.Features.Levels;
using Game.Features.Levels.Domain;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public class TurnLeftMove : TurnMove
    {
        private readonly PlayerModel _player;
        protected override InputKey RequiredKey => InputKey.TurnLeft;
        protected override RequiredState State => RequiredState.DownOrHeld;
        protected override float RotationDegrees => 90;
        protected override float Duration => _player.Config.TurnDuration;

        public TurnLeftMove(PlayerModel player, Level level, IPlayerInput playerInput, IAnimation animation, string id) : base(player, level, playerInput, animation, id) => 
            _player = player;
    }
}