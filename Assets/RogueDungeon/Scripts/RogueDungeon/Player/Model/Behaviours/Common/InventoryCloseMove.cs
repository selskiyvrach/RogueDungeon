using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours.Common
{
    public class InventoryCloseMove : PlayerMove
    {
        private readonly Player _player;
        private readonly IPlayerInput _input;
        protected override float Duration => _player.Config.OpenInventoryDuration;
        
        public InventoryCloseMove(string id, IAnimation animation, Player player, IPlayerInput input) : base(id, animation)
        {
            _player = player;
            _input = input;
        }

        public override void Enter()
        {
            base.Enter();
            if(_input.IsDown(InputKey.Inventory))
                _input.ConsumeInput(InputKey.Inventory);
            if(_input.IsDown(InputKey.Esc))
                _input.ConsumeInput(InputKey.Esc);
        }

        public override void Exit()
        {
            base.Exit();
            _player.Hands.Enable();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_input.IsDown(InputKey.Inventory) || _input.IsDown(InputKey.Esc));
    }
}