using System;
using Game.Libs.Animations;
using Game.Libs.Input;
using Game.Libs.Movesets;
using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
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

        protected override void OnAnimationEvent(string name)
        {
            base.OnAnimationEvent(name);
            if (name == AnimationEventNames.DRAW_INVENTORY)
                _player.ShowInventory();
            else
                throw new ArgumentException();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.Hands.IsIdle;
    }
}