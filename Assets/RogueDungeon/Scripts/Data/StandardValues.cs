using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Data
{
    [CreateAssetMenu(menuName = "Configs/Data/StandardValues", fileName = "StandardValues", order = 0)]
    public class StandardValues : ScriptableDictionary<StandardValues.Values>
    {
        [Serializable]
        public struct ValuePerValueType
        {
            public RelativeValue _valueRelativeValue;
            public int Value;
        }

        [Serializable]
        public class Values
        {
            [field: SerializeField] public ValuePerValueType[] Entries { get; private set;} = { new() { _valueRelativeValue = RelativeValue.VeryLow}, new() { _valueRelativeValue = RelativeValue.Low},new() { _valueRelativeValue = RelativeValue.Medium},new() { _valueRelativeValue = RelativeValue.High},new() { _valueRelativeValue = RelativeValue.VeryHigh}  };
            public int this[RelativeValue relativeValue] => Entries.First(n => n._valueRelativeValue == relativeValue).Value;
        }

        public int GetValue(Key key, ValueConfig config) =>
            config.Value == RelativeValue.Custom 
                ? config.CustomValue 
                : GetValue(key, config.Value);

        public int GetValue(Key key, RelativeValue value) =>
            this[key.ToString()][value];

        public enum Key
        {
            AttackDamage = 100,
            AttackActionDuration = 200,
            AttackActionKeyframe = 300,
        }
    }
}