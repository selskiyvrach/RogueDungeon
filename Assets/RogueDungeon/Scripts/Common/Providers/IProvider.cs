namespace Common.Providers
{
    public interface IProvider<out T>
    {
        T Get { get; }
    }
}