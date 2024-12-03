using System;
using System.Collections.Generic;

namespace Common.Prameters
{
    public class Parameters<T> where T : struct, Enum
    {
        private readonly Dictionary<T, Parameter> _parameters = new();

        public void Add(T id, Parameter parameter = null) => 
            _parameters.Add(id, parameter ?? new Parameter());

        public Parameter Get(T id) => 
            _parameters[id];

        public Parameter GetOrAdd(T id)
        {
            if(!_parameters.ContainsKey(id))
                Add(id);
            return Get(id);
        }

        public Parameters<T> FetchConfig(ParametersConfig<T> config)
        {
            config.ApplyToParameters(this);
            return this;
        }
    }
}