using System;

namespace Common.Properties
{
    public class ReadOnlyBoundProperty<T> : IReadOnlyProperty<T>
    {
        private readonly Func<T> _getter;

        public T Value => _getter.Invoke();

        public ReadOnlyBoundProperty(Func<T> getter) => 
            _getter = getter;
    }
}