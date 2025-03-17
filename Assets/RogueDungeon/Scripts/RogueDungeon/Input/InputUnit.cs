using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Input
{
    [Serializable]
    public class InputUnit
    {
        [field: SerializeField, HorizontalGroup("1")] public InputKey Key { get; private set; }
        [field: SerializeField, HorizontalGroup("2")] public KeyCode KeyCode { get; private set; }
        [field: SerializeField, HorizontalGroup("1")] public KeyState State { get; private set; }
        [field: SerializeField, HorizontalGroup("2")] public float CoyoteTime { get; private set; }

        private float _timeSinceReleased = float.PositiveInfinity;
        private bool _isReceived;
        public bool IsReceived => _isReceived || _timeSinceReleased <= CoyoteTime;

        public void Tick(float timeDelta)
        {
            _isReceived = State switch
            {
                KeyState.Down => UnityEngine.Input.GetKeyDown(KeyCode),
                KeyState.Held => UnityEngine.Input.GetKey(KeyCode),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            if(!_isReceived && _timeSinceReleased <= CoyoteTime)
                _timeSinceReleased += timeDelta;
            
            if(_isReceived)
                _timeSinceReleased = 0;
        }

        public void ResetState()
        {
            _isReceived = false;
            _timeSinceReleased = float.PositiveInfinity;
        }
    }
}