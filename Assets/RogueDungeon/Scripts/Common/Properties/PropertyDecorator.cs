using System;

namespace Common.Properties
{
    public interface IPropertyDecorator<T> : IReadOnlyPropertyDecorator<T>, IProperty<T> where T : struct, Enum
    {
        new IProperty<T> Decorated { get; set; }
    }
    
    public interface IReadOnlyPropertyDecorator<T> : IReadOnlyProperty<T>
    {
        IReadOnlyProperty<T> Decorated { get; set; }
    }

    public class PropertyDecorator<T> : IPropertyDecorator<T> where T : struct, Enum
    {
        public IProperty<T> Decorated { get; set; }

        public T Value
        {
            get => Decorated.Value;
            set => Decorated.Value = value;
        }

        public PropertyDecorator(IProperty<T> decorated = default) => 
            Decorated = decorated;

        IReadOnlyProperty<T> IReadOnlyPropertyDecorator<T>.Decorated
        {
            get => Decorated;
            set => Decorated = (IProperty<T>)value;
        }
    }
}