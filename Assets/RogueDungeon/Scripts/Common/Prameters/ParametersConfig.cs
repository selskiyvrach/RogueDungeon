using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Prameters
{
    public abstract class ParametersConfig<T> : ScriptableObject where T : struct, Enum
    {
        [SerializeField] private List<ParameterPicker<T>> _parameters;
        
        public void ApplyToParameters(Parameters<T> parameters)
        {
            foreach (var picker in _parameters) 
                parameters.GetOrAdd(picker.ParameterType).AddModifier(new Modifier(picker.ModifierType, picker.Value, this));
        }
    }
}