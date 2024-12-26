namespace Common.Parameters
{
    public interface IParameter<T> where T : IParameterDefinition
    {
    }

    public interface IParameter
    {
        float Value { get; }
    }
}