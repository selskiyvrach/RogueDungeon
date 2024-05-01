using System;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Data
{
    [CreateAssetMenu(menuName = "Configs/Data/StandardValues", fileName = "StandardValues", order = 0)]
    public class StandardValues : ScriptableObject
    {
        [Serializable]
        public struct ValuePerValueType
        {
            public StandardValue ValueType;
            public int Value;
        }

        [field: SerializeField] public ValuePerValueType[] AttackDamageValues { get; private set; } = { new() { ValueType = StandardValue.VeryLow}, new() { ValueType = StandardValue.Low},new() { ValueType = StandardValue.Medium},new() { ValueType = StandardValue.High},new() { ValueType = StandardValue.VeryHigh}  };
        [field: SerializeField] public ValuePerValueType[] AttackActionDurations { get; private set; } = { new() { ValueType = StandardValue.VeryLow}, new() { ValueType = StandardValue.Low},new() { ValueType = StandardValue.Medium},new() { ValueType = StandardValue.High},new() { ValueType = StandardValue.VeryHigh}  };
        [field: SerializeField] public ValuePerValueType[] AttackActionHitKeyframes { get; private set; } = { new() { ValueType = StandardValue.VeryLow}, new() { ValueType = StandardValue.Low},new() { ValueType = StandardValue.Medium},new() { ValueType = StandardValue.High},new() { ValueType = StandardValue.VeryHigh}  };

        public float GetValue(ValueConfig config, ValuePerValueType[] source) =>
            config.Value == StandardValue.Custom 
                ? config.CustomValue 
                : GetValue(config.Value, source);

        public float GetValue(StandardValue value, ValuePerValueType[] source) => 
            source.First(n => n.ValueType == value).Value;
    }
}