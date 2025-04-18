using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Input
{
    [Serializable]
    public class InputUnit
    {
        [field: SerializeField, HorizontalGroup]
        public InputKey Key { get; private set; }

        [field: SerializeField, HorizontalGroup]
        public KeyCode KeyCode { get; private set; }

        private float _timeSincePressed = 10;

        public bool IsReceived() =>
            UnityEngine.Input.GetKeyDown(KeyCode) || _timeSincePressed <= .5f;

        public bool IsHeld() => 
            UnityEngine.Input.GetKey(KeyCode);

        public void Tick(float timeDelta)
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode))
                _timeSincePressed = 0;
            else
                _timeSincePressed += timeDelta;
        }

        public void ResetState() => 
            _timeSincePressed = 10;
    }
}