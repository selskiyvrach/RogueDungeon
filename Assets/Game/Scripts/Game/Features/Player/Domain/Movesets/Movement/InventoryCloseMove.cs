using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public class InventoryCloseMove : PlayerMove
    {
        private readonly Player _player;
        private readonly InputUnit _inventoryKey;
        private readonly InputUnit _escKey;
        protected override float Duration => _player.Config.OpenInventoryDuration;
        
        public InventoryCloseMove(string id, IAnimation animation, Player player, IPlayerInput input) : base(id, animation)
        {
            _player = player;
            _inventoryKey = input.GetKey(InputKey.Inventory);
            _escKey = input.GetKey(InputKey.Esc);
        }

        public override void Enter()
        {
            base.Enter();
            _inventoryKey.Reset();
            _escKey.Reset();
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            // if(Animation.Progress > .3f && _player.WorldInventory.IsOpen)
            //     _player.WorldInventory.Pack();
        }

        public override void Exit()
        {
            base.Exit();
            _player.Hands.Show();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_inventoryKey.IsDown || _escKey.IsDown);
    }
}