using System;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Input
{
    internal class PlayerInput : IPlayerInput
    {
        private readonly InputMap _inputMap;

        public Vector2 CursorPos => UnityEngine.Input.mousePosition;

        public PlayerInput(InputMap inputMap) => 
            _inputMap = inputMap;

        public void Tick(float timeDelta)
        {
            foreach (var unit in _inputMap.EnabledUnits) 
                unit.Tick(timeDelta);
        }

        public bool IsDown(InputKey inputKey) => 
            _inputMap.EnabledUnits.Any(u => u.Key == inputKey && u.IsReceived());

        public bool IsHeld(InputKey inputKey) => 
            _inputMap.EnabledUnits.Any(u => u.Key == inputKey && u.IsHeld());

        public void SetFilter(InputFilter filter) => 
            _inputMap.SetFilter(filter);

        public void ConsumeInput(InputKey inputKey)
        {
            if (!IsDown(inputKey))
                throw new InvalidOperationException($"Cannot consume command of a wrong type: {inputKey}");

            _inputMap.EnabledUnits.First(n => n.Key == inputKey).ResetState();
        }
    }
}