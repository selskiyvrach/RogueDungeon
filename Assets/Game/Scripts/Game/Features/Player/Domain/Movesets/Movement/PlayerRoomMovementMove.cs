using Game.Features.Player.Domain.Movesets.Items;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public abstract class PlayerRoomMovementMove : PlayerInputMove
    {
        private readonly Player _player;

        protected PlayerRoomMovementMove(Player player, string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation, playerInput) => 
            _player = player;

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_player.IsInCombat;
    }
}