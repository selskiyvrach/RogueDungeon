namespace Common.Properties
{
    public interface IPropertyDecorator<T> : IReadOnlyPropertyDecorator<T>, IProperty<T>
    {
        new IProperty<T> Decorated { get; set; }
    }
    
    public interface IReadOnlyPropertyDecorator<T> : IReadOnlyProperty<T>
    {
        IReadOnlyProperty<T> Decorated { get; set; }
    }

    public class PropertyDecorator<T> : IPropertyDecorator<T>
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