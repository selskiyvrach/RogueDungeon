using System;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Input
{
    internal class PlayerInput : IPlayerInput
    {
        private readonly InputMap _inputMap;

        public Vector2 CursorPos { get; private set; }
        public event Action OnCursorMoved; 

        public PlayerInput(InputMap inputMap) => 
            _inputMap = inputMap;

        public void Tick(float timeDelta)
        {
            var newCursorPos = (Vector2)UnityEngine.Input.mousePosition;
            if (CursorPos != newCursorPos)
            {
                CursorPos = newCursorPos;
                OnCursorMoved?.Invoke();
            }

            foreach (var unit in _inputMap.EnabledUnits) 
                unit.Tick(timeDelta);
        }

        public InputUnit GetKey(InputKey key) => 
            _inputMap.AllUnits.First(n => n.Key == key);
    }
}