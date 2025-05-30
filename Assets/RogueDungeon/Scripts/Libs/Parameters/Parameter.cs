using System;

namespace Libs.Parameters
{
    public class Parameter : IParameter
    {
        private readonly Func<float> _getter;
        public float Value => _getter.Invoke();

        public Parameter(Func<float> value) => 
            _getter = value;
    }

    public class Parameter<T> : Parameter, IParameter<T> where T : IParameterDefinition
    {
        public Parameter(Func<float> value) : base(value)
        {
        }
    }
}