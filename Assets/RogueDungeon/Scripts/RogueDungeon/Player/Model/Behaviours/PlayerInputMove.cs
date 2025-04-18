using System;
using Common.Animations;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours
{
    public abstract class PlayerInputMove : PlayerMove
    {
        [Flags]
        protected enum RequiredState
        {
            None = 0,
            Down = 1,
            Held = 2,
            DownOrHeld = Down | Held,
        }

        private readonly IPlayerInput _playerInput;

        protected abstract InputKey RequiredKey { get; }
        protected abstract RequiredState State { get; } 

        protected PlayerInputMove(string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation) => 
            _playerInput = playerInput;

        public override void Enter()
        {
            if(_playerInput.IsDown(RequiredKey))
                _playerInput.ConsumeInput(RequiredKey);
            base.Enter();
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && State switch
            {
                RequiredState.Down => _playerInput.IsDown(RequiredKey),
                RequiredState.Held => _playerInput.IsHeld(RequiredKey),
                RequiredState.DownOrHeld => _playerInput.IsDown(RequiredKey) || _playerInput.IsHeld(RequiredKey),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}