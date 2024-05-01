using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Data
{
    [Serializable]
    public class ValueConfig
    {
        [field: SerializeField] public StandardValue Value { get; private set; } = StandardValue.Medium;
        [field: ShowIf("@Value == StandardValue.Custom"), SerializeField] public int CustomValue { get; private set; }
    }
}