using System;
using System.Linq;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace RogueDungeon.Data
{
    [CreateAssetMenu(menuName = "Configs/Data/StandardValues", fileName = "StandardValues", order = 0)]
    public class StandardValues : ScriptableDictionary<StandardValues.Values>
    {
        private static StandardValues _instance;
        private static StandardValues Instance => _instance ??= Resources.Load<StandardValues>("Configs/Data/StandardValues");
        
        [Serializable]
        public struct ValuePerValueType
        {
            public RelativeValue RelativeValue;
            public int Value;
        }

        [Serializable]
        public class Values
        {
            [field: SerializeField] public ValuePerValueType[] Entries { get; private set;} = { new() { RelativeValue = RelativeValue.VeryLow}, new() { RelativeValue = RelativeValue.Low},new() { RelativeValue = RelativeValue.Medium},new() { RelativeValue = RelativeValue.High},new() { RelativeValue = RelativeValue.VeryHigh}  };
            public int this[RelativeValue relativeValue] => Entries.First(n => n.RelativeValue == relativeValue).Value;
        }

        public static bool HasStat(string statId) => 
            Instance.TryGetValue(statId, out var _);

        public static float GetValue(string statId, RelativeValue value) =>
            Instance[statId][value];
    }
}