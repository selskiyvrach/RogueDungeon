using System;
using System.Collections.Generic;

namespace Common.Prameters
{
    public class ParametersAggregate : IParameters
    {
        private readonly Dictionary<object, object> _parametersMap = new();
        
        public Parameter Get<T>(T id) where T : struct, Enum => 
            Get<T>().Get(id);
        
        public Parameters<T> Get<T>() where T : struct, Enum => 
            _parametersMap.TryGetValue(typeof(T), out var parameters) ? (Parameters<T>)parameters : null;

        public void Add<T>(T id, Parameter parameter) where T : struct, Enum
        {
            if(!_parametersMap.ContainsKey(typeof(T)))
                _parametersMap.Add(typeof(T), new Parameters<T>());
            Get<T>().Add(id, parameter);   
        }

        public ParametersAggregate FetchConfig<T>(ParametersConfig<T> config) where T : struct, Enum
        {
            Get<T>().FetchConfig(config);
            return this;
        }
    }
}