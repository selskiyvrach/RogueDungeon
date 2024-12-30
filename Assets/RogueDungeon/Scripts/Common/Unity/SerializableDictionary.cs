using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.UtilsUnity
{
    [Serializable]
    public abstract class SerializableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        [Serializable]
        public struct KeyValuePair<TKey, TValue>
        {
            [HideLabel, HorizontalGroup]public TKey Key;
            [HideLabel, HorizontalGroup]public TValue Value;

            public System.Collections.Generic.KeyValuePair<TKey, TValue> ToSystemType() => 
                new(Key, Value);
        }

        [SerializeField] private List<KeyValuePair<TKey, TValue>> _elements;
        private Dictionary<TKey, TValue> _dictionary;

        public int Count => GetDictionary().Count;
        public IEnumerable<TKey> Keys => GetDictionary().Keys;
        public IEnumerable<TValue> Values => GetDictionary().Values;
        public TValue this[TKey key]
        {
            get => GetDictionary()[key];
            set => GetDictionary()[key] = value;
        }

        private Dictionary<TKey, TValue> GetDictionary() => 
            _dictionary ??= _elements.ToDictionary(n => n.Key, n => n.Value);

        public IEnumerator<System.Collections.Generic.KeyValuePair<TKey, TValue>> GetEnumerator() => 
            GetDictionary().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();

        public bool ContainsKey(TKey key) => 
            GetDictionary().ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default;
            if (!GetDictionary().TryGetValue(key, out var gottenValue))
                return false;
            value = gottenValue;
            return true;
        }
    }
}