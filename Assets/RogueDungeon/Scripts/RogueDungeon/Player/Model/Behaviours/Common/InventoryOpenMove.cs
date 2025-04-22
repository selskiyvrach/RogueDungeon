using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class InventoryOpenMove : PlayerInputMove
    {
        private readonly Player _player;
        protected override float Duration => _player.Config.OpenInventoryDuration; 
        protected override InputKey RequiredKey => InputKey.Inventory;
        protected override RequiredState State => RequiredState.Down;
        
        public InventoryOpenMove(string id, IAnimation animation, IPlayerInput playerInput, Player player) : base(id, animation, playerInput) => 
            _player = player;

        public override void Enter()
        {
            base.Enter();
            _player.Hands.Disable();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Hands.IsIdle;
    }
}