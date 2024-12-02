using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Prameters
{
    public class Parameters<T> where T : struct, Enum
    {
        private readonly List<(T id, Parameter parameter)> _parameters = new();

        public void Add(T id, Parameter parameter = null) => 
            _parameters.Add((id, parameter ?? new Parameter()));

        public Parameter Get(T id) => 
            _parameters.First(n => n.id.Equals(id)).parameter;
    }
}