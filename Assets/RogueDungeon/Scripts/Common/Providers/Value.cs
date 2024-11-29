namespace Common.Providers
{
    public class Value<T> : IValue<T>
    {
        public T value { get; set; }

        public Value(T value = default) => 
            this.value = value;
    }
}