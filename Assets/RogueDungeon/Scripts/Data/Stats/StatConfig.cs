using System;
using UnityEngine;

namespace RogueDungeon.Data.Stats
{
    [Serializable]
    public struct StatConfig
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public float Value { get; private set; }

        public StatConfig(string id, float value)
        {
            Id = id;
            Value = value;
        }

        public static StatConfig operator +(StatConfig a, StatConfig b) => 
            new(a.Id, a.Value + b.Value);

        public static StatConfig operator -(StatConfig a, StatConfig b) => 
            new(a.Id, a.Value - b.Value);
    }
}