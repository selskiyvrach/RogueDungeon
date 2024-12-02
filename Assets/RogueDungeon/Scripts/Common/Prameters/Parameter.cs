using System.Collections.Generic;

namespace Common.Prameters
{
    public sealed class Parameter
    {
        private readonly List<Modifier> _modifiers = new(10);
        private readonly Modifier _default = default;
        private bool _isDirty;
        private float _value;

        public float Value
        {
            get
            {
                if (_isDirty)
                    CalculateValue();
                return _value;
            }
        }

        public Parameter(float value) => 
            AddModifier(new Modifier(value, this));

        public Parameter()
        {
        }

        private void CalculateValue()
        {
            var flatTotal = 0f;
            var percentTotal = 1f;
            
            foreach (var modifier in _modifiers)
            {
                if(modifier.Equals(_default))
                    continue;
                if (modifier.type == Modifier.Type.Flat)
                    flatTotal += modifier.Value;
                else
                    percentTotal *= 1 + modifier.Value;
            }

            _value = flatTotal * percentTotal;
            _isDirty = false;
        }

        public void AddModifier(Modifier modifier)
        {
            for (var i = 0; i < _modifiers.Count; i++)
            {   
                if (!_modifiers[i].Equals(_default))
                    continue;
                _modifiers[i] = modifier;
                _isDirty = true;
                return;
            }
            _modifiers.Add(modifier);
            _isDirty = true;
            
        }

        public void RemoveAllFromSource(object source)
        {
            for (var i = 0; i < _modifiers.Count; i++)
            {
                var modifier = _modifiers[i];
                if (modifier.Source != source)
                    continue;
                _modifiers[i] = _default;
                _isDirty = true;
            }
        }
    }
}