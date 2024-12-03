using System;
using System.Collections.Generic;

namespace Common.Prameters
{
    public class ParametersAggregate : IParameters
    {
        private readonly Dictionary<object, object> _parametersMap = new();
        
        public Parameter Get<T>(T id) where T : struct, Enum => 
            _parametersMap.TryGetValue(id, out var parameters) ? ((Parameters<T>)parameters).Get(id) : null;

        public void Add<T>(T id, Parameter parameter) where T : struct, Enum
        {
            if(!_parametersMap.ContainsKey(id))
                _parametersMap.Add(id, new Parameters<T>());
            ((Parameters<T>)_parametersMap[id]).Add(id, parameter);   
        }
    }
}