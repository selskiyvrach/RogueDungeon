using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Keys
{
    [Serializable]
    public class KeyPicker<T> : KeyPicker
    {
        protected override List<ValueDropdownItem<string>> GetAvailableKeys() =>
            typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(Key)).Select(field => new ValueDropdownItem<string>(field.Name, field.Name)).ToList();

        protected override Key GetValue()
        {
            var field = typeof(T).GetField(_pickedName, BindingFlags.Public | BindingFlags.Static);
            if (field == null || field.FieldType != typeof(Key))
                throw new InvalidOperationException($"Field '{_pickedName}' does not exist or is not of type Key.");

            return (Key)field.GetValue(default);
        }
    }

    [Serializable]
    public abstract class KeyPicker : IEquatable<Key>
    {
        [ValueDropdown(nameof(GetAvailableKeys), NumberOfItemsBeforeEnablingSearch = 0)]
        [SerializeField, HideLabel]
        protected string _pickedName = Key.NONE_NAME;

        protected abstract List<ValueDropdownItem<string>> GetAvailableKeys();
        protected abstract Key GetValue();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((KeyPicker)obj);
        }

        public static implicit operator Key(KeyPicker picker) => picker.GetValue();
        public override int GetHashCode() => _pickedName?.GetHashCode() ?? 0;
        public static bool operator ==(KeyPicker picker, Key key) => picker?.GetValue() == key;
        public static bool operator !=(KeyPicker picker, Key key) => !(picker == key);
        public static bool operator ==(KeyPicker left, KeyPicker right) => left?.Equals(right) ?? ReferenceEquals(right, null);
        public static bool operator !=(KeyPicker left, KeyPicker right) => !(left == right);
        public bool Equals(Key other) => 
            this == other;

        public override string ToString() => GetValue().ToString();
    }
}