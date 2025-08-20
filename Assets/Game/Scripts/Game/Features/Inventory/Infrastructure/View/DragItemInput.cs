using System;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.Input;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class DragItemInput : IDragItemInput
    {
        private readonly IPlayerInput _playerInput;

        public event Action OnPointerDown;
        public event Action OnPointerUp;
        public event Action OnMoved;

        public DragItemInput(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
        }
    }
}