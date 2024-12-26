namespace Common.Parameters
{
    public interface IParameter<T> : IParameter where T : IParameterDefinition
    {
    }

    public interface IParameter
    {
        float Value { get; }
    }
}