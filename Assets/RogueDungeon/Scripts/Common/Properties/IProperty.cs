using System;

namespace Common.Properties
{
    public interface IProperty<T> : IReadOnlyProperty<T> where T : struct, Enum
    {
        public new T Value { get; set; }
    }
    
    
}