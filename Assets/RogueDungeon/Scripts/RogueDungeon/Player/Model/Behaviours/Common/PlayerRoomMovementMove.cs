using Common.Animations;
using Input;
using Levels;
using Moves;

namespace Player.Model.Behaviours.Common
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