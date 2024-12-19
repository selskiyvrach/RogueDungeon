using System;

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
        private readonly Func<float> _getter;
        public float Value => _getter.Invoke();

        protected Parameter(Func<float> value) => 
            _getter = value;
    }
}