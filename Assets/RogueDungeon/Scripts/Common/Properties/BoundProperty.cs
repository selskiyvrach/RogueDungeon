using System;

namespace Common.Properties
{
    public class BoundProperty<T> : Property<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;

        public new T Value
        {
            get => _getter.Invoke();
            set => _setter.Invoke(value);
        }

        public BoundProperty(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }
    }
}