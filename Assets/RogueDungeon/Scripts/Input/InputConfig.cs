using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Input
{
    [CreateAssetMenu(menuName = "Configs/InputConfig", fileName = "InputConfig", order = 0)]
    public class InputConfig : ScriptableObject
    {
        [Serializable]
        public class UnitConfig
        {
            [HorizontalGroup, HideLabel] public Action Action;
            [HorizontalGroup, HideLabel] public KeyCode KeyCode;
        }

        [Serializable]
        public class ModeUnits
        {
            [HideLabel] public Mode Mode;
            public UnitConfig[] Units;
        }

        [SerializeField] private ModeUnits[] _modes;

        public IEnumerable<(Mode mode, UnitConfig unitConfig)> GetUnitsByModes() => 
            from mode in _modes from unit in mode.Units select (mode.Mode, unit);
    }
}