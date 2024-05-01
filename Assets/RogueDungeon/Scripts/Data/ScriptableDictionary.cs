using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Data
{
    public abstract class ScriptableDictionary<T> : ScriptableObject
    {
        [Serializable]
        public struct KeyValuePair
        {
            [field: HideLabel, SerializeField] public string Key { get; private set; }
            [field: SerializeField] public T Value { get; private set; }
        }

        [LabelText("@Label"), SerializeField] private KeyValuePair[] _entries;

        protected virtual string Label { get; } = "Entries";

        public T this[string key] => _entries.First(n => n.Key == key).Value;
        
        public bool ContainsKey(string key) => 
            _entries.Any(n => n.Key == key);

        public bool TryGetValue(string key, out T value)
        {
            foreach (var entry in _entries)
            {
                if (entry.Key != key)
                    continue;
                value = entry.Value;
                return true;
            }

            value = default;
            return false;
        }
    }
}