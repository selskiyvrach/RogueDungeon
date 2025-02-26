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
        public bool IsReceived => State switch {
            KeyState.Down => UnityEngine.Input.GetKeyDown(KeyCode),
            KeyState.Held => UnityEngine.Input.GetKey(KeyCode),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        public static InputFilter Empty => new();
    }
}