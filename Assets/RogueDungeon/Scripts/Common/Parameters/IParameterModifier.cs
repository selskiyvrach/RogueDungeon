namespace Common.Parameters
{
    public interface IParameterModifier<T> where T : IParameterDefinition
    {
        float GetModifiedValue(IParameter<T> baseValue);
    }
}