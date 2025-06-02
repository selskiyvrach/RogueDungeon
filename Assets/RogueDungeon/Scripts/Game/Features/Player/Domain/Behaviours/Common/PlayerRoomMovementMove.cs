using Game.Libs.Input;
using Game.Libs.Movesets;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.Common
{
    public abstract class PlayerRoomMovementMove : PlayerInputMove
    {
        private readonly ICurrentRoomCanLeaveReader _canLeaveReader;

        protected PlayerRoomMovementMove(ICurrentRoomCanLeaveReader level, string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation, playerInput) => 
            _canLeaveReader = level;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _canLeaveReader.CanLeave;
    }

    public interface ICurrentRoomCanLeaveReader
    {
        bool CanLeave { get; }
    }
}