using Game.Features.Levels;
using Game.Features.Levels.Domain;
using Game.Libs.Input;
using Game.Libs.Movesets;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public abstract class PlayerRoomMovementMove : PlayerInputMove
    {
        private readonly Level _level;

        protected PlayerRoomMovementMove(Level level, string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation, playerInput) => 
            _level = level;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _level.CurrentRoom.CanLeave;
    }
}