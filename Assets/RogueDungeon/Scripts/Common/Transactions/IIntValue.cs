namespace Common.Transactions
{
    public interface IIntValue : INumericValue
    {
        int Value { get; }
    }
}