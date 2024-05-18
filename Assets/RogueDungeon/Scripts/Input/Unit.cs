using UnityEngine;

namespace RogueDungeon.Input
{
    public class Unit : IUnit
    {
        private readonly KeyCode _keyCode;
        public bool Down => UnityEngine.Input.GetKeyDown(_keyCode);
        public bool Up => UnityEngine.Input.GetKeyUp(_keyCode);
        public bool Held => UnityEngine.Input.GetKey(_keyCode);
        public Mode Modes { get; }
        public Unit(KeyCode keyCode, Mode modes)
        {
            _keyCode = keyCode;
            Modes = modes;
        }
    }
}