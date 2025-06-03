using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class TurnLeftMove : TurnMove
    {
        private readonly Player _player;
        protected override InputKey RequiredKey => InputKey.TurnLeft;
        protected override RequiredState State => RequiredState.DownOrHeld;
        protected override float RotationDegrees => 90;
        protected override float Duration => _player.Config.TurnDuration;

        public TurnLeftMove(Player player, ILevelTraverser levelTraverser, IPlayerInput playerInput, IAnimation animation, string id) : base(player, levelTraverser, playerInput, animation, id) => 
            _player = player;
    }
}