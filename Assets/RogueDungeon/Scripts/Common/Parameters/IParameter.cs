namespace Common.Parameters
{
    public interface IParameter<T> where T : IParameter
    {
    }

    public interface IParameter
    {
        float Value { get; }
    }
}