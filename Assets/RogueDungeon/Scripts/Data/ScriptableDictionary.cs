using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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

        // TODO: add recursive reference check
        [SerializeField] private ScriptableDictionary<T>[] _extensions;

        [LabelText("@Label"), SerializeField] private KeyValuePair[] _entries;

        protected virtual string Label { get; } = "Entries";

        public T this[string key] => 
            TryGetValue(key, out var value) ? value : throw new InvalidOperationException($"No such key '{key}' is present in {name}");

        public bool TryGetValue(string key, out T value)
        {
            foreach (var entry in _entries)
            {
                if (entry.Key != key)
                    continue;
                value = entry.Value;
                return true;
            }

            foreach (var extension in _extensions ?? Enumerable.Empty<ScriptableDictionary<T>>())
            {
                if (!extension.TryGetValue(key, out var valueFromExtension))
                    continue;
                value = valueFromExtension;
                return true;
            }

            value = default;
            return false;
        }
    }
}