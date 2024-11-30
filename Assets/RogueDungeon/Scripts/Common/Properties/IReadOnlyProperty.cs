namespace Common.Properties
{
    public interface IReadOnlyProperty<out T>
    {
        public T Value { get; }
    }
}