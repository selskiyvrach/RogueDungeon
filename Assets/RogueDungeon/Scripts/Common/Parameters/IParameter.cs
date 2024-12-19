namespace Common.Parameters
{
    public interface IParameter<T> where T : IParameter
    {
    }

    public interface IParameter
    {
        float Value { get; }
    }

    public class Parameter : IParameter
    {
        public float Value { get; }

        public Parameter(float value) => 
            Value = value;
    }
}