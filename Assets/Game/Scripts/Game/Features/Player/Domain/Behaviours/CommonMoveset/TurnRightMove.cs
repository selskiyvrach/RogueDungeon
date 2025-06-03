using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class TurnRightMove : TurnMove
    {
        private readonly Player _player;
        protected override InputKey RequiredKey => InputKey.TurnRight;
        protected override float RotationDegrees => -90;
        protected override float Duration => _player.Config.TurnDuration;

        public TurnRightMove(Player player, ILevelTraverser levelTraverser, IPlayerInput playerInput, IAnimation animation, string id) : base(player, levelTraverser, playerInput, animation, id) => 
            _player = player;
    }
}