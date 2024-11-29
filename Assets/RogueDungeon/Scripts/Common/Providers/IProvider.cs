namespace Common.Providers
{
    public interface IProvider<out T>
    {
        T Item { get; }
    }

    public interface IValue<T> : IProvider<T>
    {
        T Item { set; }
    }
}