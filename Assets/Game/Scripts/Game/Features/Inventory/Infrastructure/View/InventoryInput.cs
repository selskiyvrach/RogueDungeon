using System;
using Game.Features.Inventory.App.Presenters;
using Game.Libs.Input;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryInput : IInventoryInput
    {
        private readonly IPlayerInput _playerInput;
        private readonly InputUnit _button;

        public event Action OnPointerDown
        {
            add => _button.OnDown += value;
            remove => _button.OnDown -= value;
        }

        public event Action OnPointerUp
        {
            add => _button.OnUp += value;
            remove => _button.OnUp -= value;
        }

        public event Action OnMoved
        {
            add => _playerInput.OnCursorMoved += value;
            remove => _playerInput.OnCursorMoved -= value;
        }

        public Vector2 ScreenPosition => _playerInput.CursorPos;

        public InventoryInput(IPlayerInput playerInput)
        {
            _playerInput = playerInput;
            _button = _playerInput.GetKey(InputKey.DragItem);
        }
    }
}