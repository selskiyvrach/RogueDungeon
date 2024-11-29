namespace Common.Providers
{
    public interface IProvider<out T>
    {
        T value { get; }
    }

    public interface IValue<T> : IProvider<T>
    {
        T value { set; }
    }
}