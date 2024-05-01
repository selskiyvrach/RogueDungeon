using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Data
{
    [Serializable]
    public class ValueConfig
    {
        [field: SerializeField] public RelativeValue Value { get; private set; } = RelativeValue.Medium;
        [field: ShowIf("@Value == RelativeValue.Custom"), SerializeField] public int CustomValue { get; private set; }
    }
}