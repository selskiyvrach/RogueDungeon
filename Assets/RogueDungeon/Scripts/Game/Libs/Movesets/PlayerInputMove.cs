using System;
using Game.Libs.Input;
using Libs.Animations;
using Libs.Movesets;

namespace Game.Libs.Movesets
{
    public abstract class PlayerInputMove : Move
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
        
        private bool _skip => RequiredKey == InputKey.None && State == RequiredState.None;

        protected PlayerInputMove(string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation) => 
            _playerInput = playerInput;

        public override void Enter()
        {
            if (!_skip)
                _playerInput.GetKey(RequiredKey).Reset();
            base.Enter();
        }

        protected override bool CanTransitionTo()
        {
            var inputUnit = _skip ? null : _playerInput.GetKey(RequiredKey);
            return base.CanTransitionTo() && _skip || State switch
            {
                RequiredState.Down => inputUnit.IsDown,
                RequiredState.Held => inputUnit.IsHeld,
                RequiredState.DownOrHeld => inputUnit.IsDown || inputUnit.IsHeld,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}