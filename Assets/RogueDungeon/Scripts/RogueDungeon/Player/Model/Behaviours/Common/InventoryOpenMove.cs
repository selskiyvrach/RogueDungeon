using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class InventoryOpenMove : PlayerInputMove
    {
        private readonly Player _player;
        private bool _inventoryIsOpen;
        protected override float Duration => _player.Config.OpenInventoryDuration; 
        protected override InputKey RequiredKey => InputKey.Inventory;
        protected override RequiredState State => RequiredState.Down;
        
        public InventoryOpenMove(string id, IAnimation animation, IPlayerInput playerInput, Player player) : base(id, animation, playerInput) => 
            _player = player;

        public override void Enter()
        {
            base.Enter();
            _player.Hands.Disable();
            _inventoryIsOpen = false;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if (_inventoryIsOpen || Animation.Progress < .3f)
                return;
            
            _player.WorldInventory.Unpack();
            _inventoryIsOpen = true;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Hands.IsIdle;
    }
}