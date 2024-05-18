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
            [HorizontalGroup, HideLabel] public Mode Modes;
            [HorizontalGroup(width: .3f), HideLabel] public Action Action;
            [HorizontalGroup(width: .2f), HideLabel] public KeyCode KeyCode;
        }

        [field: SerializeField] public UnitConfig[] Units { get; private set; }
    }
}