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
            AddModifier(new Modifier(Modifier.Type.Flat, value, this));

        public Parameter()
        {
        }

        private void CalculateValue()
        {
            var flatTotal = 0f;
            var percentTotal = 1f;
            
            foreach (var modifier in _modifiers)
            {
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
            _modifiers.Add(modifier);
            _isDirty = true;
        }
        
        public void RemoveOneFromSource(object source)
        {
            for (var i = _modifiers.Count - 1; i >= 0; i--)
            {
                var modifier = _modifiers[i];
                if (modifier.Source != source)
                    continue;
                _modifiers.RemoveAt(i);
                _isDirty = true;
                return;
            }
        }

        public void RemoveAllFromSource(object source)
        {
            for (var i = _modifiers.Count - 1; i >= 0; i--)
            {
                var modifier = _modifiers[i];
                if (modifier.Source != source)
                    continue;
                _modifiers.RemoveAt(i);
                _isDirty = true;
            }
        }
    }
}