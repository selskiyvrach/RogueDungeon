using System;
using Common.UtilsDotNet;
using UnityEngine;

namespace RogueDungeon.Input
{
    internal class PlayerInput : IPlayerInput
    {
        private readonly InputMap _inputMap;
        private InputUnit _currentInput;
        private float _timeSinceReleased;
        private float _timeHeld;

        public PlayerInput(InputMap inputMap) => 
            _inputMap = inputMap;

        public void Tick(float timeDelta)
        {
            UpdateCoyoteTime();
            ReadCommands(timeDelta);
        }

        public bool HasInput(InputKey inputKey) => 
            _currentInput?.Key == inputKey.ThrowIfNone();

        public void SetFilter(InputFilter filter)
        {
            _inputMap.SetFilter(filter);
            ResetCurrentState();
        }

        public void ConsumeInput(InputKey inputKey)
        {
            if (!HasInput(inputKey))
                throw new InvalidOperationException($"Cannot consume command of a wrong type. Current: {_currentInput}, consuming: {inputKey}");

            ResetCurrentState();
        }

        private void ResetCurrentState()
        {
            _currentInput = null;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
        }

        private void ReadCommands(float timeDelta)
        {
            foreach (var unit in _inputMap.EnabledUnits)
            {
                if (!unit.IsReceived) 
                    continue;
                
                _timeSinceReleased = 0;
                if (unit != _currentInput)
                {
                    _currentInput = unit;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += timeDelta;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_currentInput == null) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= _currentInput.CoyoteTime) 
                _currentInput = null;
        }
    }
}