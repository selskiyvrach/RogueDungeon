using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Input
{
    [Serializable]
    public class InputUnit
    { 
        [field: SerializeField, HorizontalGroup] public InputKey Key { get; private set; }
        [field: SerializeField, HorizontalGroup] public KeyCode KeyCode { get; private set; }
        private float _timeSincePressed = 10;
        private bool _isDown;

        public event Action OnDown;
        public event Action OnUp;
        public event Action OnBeingHeld;

        public bool IsDown => _isDown || _timeSincePressed < .5f;
        public bool IsHeld { get; private set; }
        public bool IsUp { get; private set; }

        public void Tick(float timeDelta)
        {
            ResetButtonStates();
            
            if (UnityEngine.Input.GetKeyDown(KeyCode))
            {
                _isDown = true;
                _timeSincePressed = 0;
                OnDown?.Invoke();
            }
            else
                _timeSincePressed += timeDelta;

            if (UnityEngine.Input.GetKeyUp(KeyCode))
            {
                IsUp = true;
                OnUp?.Invoke();
            }

            if (UnityEngine.Input.GetKey(KeyCode))
            {
                IsHeld = true;
                OnBeingHeld?.Invoke();
            }
        }

        public void Reset()
        {
            ResetButtonStates();
            _timeSincePressed = float.MaxValue;
        }

        private void ResetButtonStates() => 
            _isDown = IsUp = IsHeld = false;
    }
}