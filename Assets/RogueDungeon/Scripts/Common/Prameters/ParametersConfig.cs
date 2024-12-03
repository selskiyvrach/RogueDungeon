using System;
using System.Collections.Generic;
using System.Linq;
using Common.DotNetUtils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Common.Prameters
{
    public abstract class ParametersConfig<T> : ParametersConfig where T : struct, Enum
    {
        [SerializeField, PropertyOrder(10)] private List<ParameterPicker<T>> _parameters;
        
        [Button, HorizontalGroup, PropertyOrder(0)]
        private void Fill() => 
            _parameters.AddRange(default(T).GetValues().Except(new []{default(T)})
                .Where(n => !_parameters.Any(m => m.ParameterType.Equals(n))).Select(n => new ParameterPicker<T> {ParameterType = n}));
        
        [Button, HorizontalGroup, PropertyOrder(1)]
        private void SortAlph() => 
            _parameters = _parameters.OrderBy(n => n.ParameterType.ToString()).ToList();
        
        [Button, HorizontalGroup, PropertyOrder(2)]
        private void SortVal() => 
            _parameters = _parameters.OrderBy(n => n.Value).ToList();
        
        
        public void ApplyToParameters(Parameters<T> parameters)
        {
            foreach (var picker in _parameters) 
                parameters.GetOrAdd(picker.ParameterType).AddModifier(new Modifier(picker.ModifierType, picker.Value, this));
        }

        public override void ApplyToParameters(ParametersAggregate aggregate) => 
            aggregate.Get<T>().FetchConfig(this);
    }

    public abstract class ParametersConfig : ScriptableObject
    {
        public abstract void ApplyToParameters(ParametersAggregate aggregate);
    }
}