using System;
using System.Collections.Generic;

namespace RogueDungeon
{
    public struct Lazy<T> where T : class, new()
    {
        private readonly Func<T> _factoryDelegate;
        private T _value;
        public T Value => _value ??= _factoryDelegate?.Invoke() ?? new T();
        public T WeakValue => _value;

        public Lazy(T value = null, Func<T> factoryDelegate = null)
        {
            _value = value;
            _factoryDelegate = factoryDelegate;
        }
        
        public static implicit operator Lazy<T>(T item) => 
            new() {_value = item};

        public static bool operator ==(Lazy<T> left, Lazy<T> right) => 
            left._value.Equals(right._value);

        public static bool operator !=(Lazy<T> left, Lazy<T> right) =>
            !(left == right);

        public bool Equals(Lazy<T> other) => 
            EqualityComparer<T>.Default.Equals(_value, other._value);

        public override bool Equals(object obj) => 
            obj is Lazy<T> other && Equals(other);

        public override int GetHashCode() => 
            EqualityComparer<T>.Default.GetHashCode(_value);
    }
}