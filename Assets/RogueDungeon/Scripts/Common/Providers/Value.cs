namespace Common.Providers
{
    public class Value<T> : IValue<T>
    {
        public T Item { get; set; }

        public Value(T item = default) => 
            Item = item;
    }
}