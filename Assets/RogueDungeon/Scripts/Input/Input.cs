using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Input
{
    public class Input
    {
        private static readonly Dictionary<Action, Unit> _units;
        private static readonly IUnit _falseUnit = new Unit(KeyCode.None, Mode.None);
        private static Mode _currentMode;

        static Input() =>
            _units = Resources.Load<InputConfig>("Configs/InputConfig").Units.ToDictionary(n => n.Action, n => new Unit(n.KeyCode, n.Modes));

        public static IUnit GetUnit(Action action) => 
            _units.TryGetValue(action, out var value) && (value.Modes & _currentMode) != 0 
                ? value 
                : _falseUnit;

        public static void SetModeState(Mode mode)
        {
            Assert.IsTrue((mode & (mode - 1)) == 0);
            _currentMode = mode;
        }
    }
}