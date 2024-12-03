using System;

namespace Common.Properties
{
    public abstract class Property
    {
    }

    public class Property<T> : Property, IProperty<T> where T : struct, Enum
    {
        public T Value { get; set; }

        public Property(T value = default) => 
            Value = value;
    }
}