namespace Common.Providers
{
    public class DummyProvider<T> : IProvider<T>
    {
        public T Get { get; set; }

        public DummyProvider(T get = default) => 
            Get = get;
    }
}