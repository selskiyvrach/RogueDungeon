namespace Common.Providers
{
    public interface IProviderDecorator<T> : IProvider<T> 
    {
        public IProvider<T> DecoratedProvider { get; set; }
    }

    public class ProviderDecorator<T> : IProviderDecorator<T> 
    {
        public T Item => DecoratedProvider.Item;
        public IProvider<T> DecoratedProvider { get; set; }

        public ProviderDecorator(IProvider<T> decoratedProvider = default) => 
            DecoratedProvider = decoratedProvider;
    }
}