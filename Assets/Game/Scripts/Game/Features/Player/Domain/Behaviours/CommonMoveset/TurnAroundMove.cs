using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class TurnAroundMove : TurnMove
    {
        private readonly Player _player;
        protected override InputKey RequiredKey => InputKey.TurnAround;
        protected override float RotationDegrees => 180;
        protected override float Duration => _player.Config.TurnAroundDuration;

        public TurnAroundMove(Player player, ILevelTraverser level, IPlayerInput playerInput, IAnimation animation, string id) : base(player, level, playerInput, animation, id) => 
            _player = player;
    }
}