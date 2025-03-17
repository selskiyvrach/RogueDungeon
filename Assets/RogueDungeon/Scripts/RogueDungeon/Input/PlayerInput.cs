using System;
using System.Collections.Generic;
using System.Linq;
using Common.UtilsDotNet;
using UnityEngine;

namespace RogueDungeon.Input
{
    internal class PlayerInput : IPlayerInput
    {
        private readonly InputMap _inputMap;

        public PlayerInput(InputMap inputMap) => 
            _inputMap = inputMap;

        public void Tick(float timeDelta)
        {
            foreach (var unit in _inputMap.EnabledUnits) 
                unit.Tick(timeDelta);
        }

        public bool HasInput(InputKey inputKey) => 
            _inputMap.EnabledUnits.Any(u => u.Key == inputKey && u.IsReceived);

        public void SetFilter(InputFilter filter) => 
            _inputMap.SetFilter(filter);

        public void ConsumeInput(InputKey inputKey)
        {
            if (!HasInput(inputKey))
                throw new InvalidOperationException($"Cannot consume command of a wrong type: {inputKey}");

            _inputMap.EnabledUnits.First(n => n.Key == inputKey).ResetState();
        }
    }
}