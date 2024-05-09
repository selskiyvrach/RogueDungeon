using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Input
{
    public class Input
    {
        private class Unit : IUnit
        {
            private readonly KeyCode _keyCode;
            public bool Down => UnityEngine.Input.GetKeyDown(_keyCode);
            public bool Up => UnityEngine.Input.GetKeyUp(_keyCode);
            public bool Held => UnityEngine.Input.GetKey(_keyCode);
            public Unit(KeyCode keyCode) => 
                _keyCode = keyCode;
        }

        private static readonly Dictionary<Mode, bool> _modeStates;
        private static readonly Dictionary<Action, Unit> _units;
        private static readonly Dictionary<Action, Mode> _modes;
        private static readonly IUnit _falseUnit = new Unit(KeyCode.None);

        static Input()
        {
            var config = Resources.Load<InputConfig>("Configs/InputConfig");
            _units = new Dictionary<Action, Unit>();
            _modes = new Dictionary<Action, Mode>();
            _modeStates = new Dictionary<Mode, bool>();
            foreach (var (mode, unitConfig) in config.GetUnitsByModes())
            {
                var unit = new Unit(unitConfig.KeyCode);
                _units.Add(unitConfig.Action, unit);
                _modes.Add(unitConfig.Action, mode);
                _modeStates.TryAdd(mode, false);
            }
        }

        public static IUnit GetUnit(Action action) => _modeStates[_modes[action]] && _units.TryGetValue(action, out var value) ? value : _falseUnit;

        public static void SetModeState(Mode mode, bool enabled) => 
            _modeStates[mode] = enabled;
    }
}