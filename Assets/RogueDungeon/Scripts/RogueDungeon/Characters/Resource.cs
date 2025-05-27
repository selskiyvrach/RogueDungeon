using System;
using UnityEngine;

namespace Characters
{
    public class Resource : IResource
    {
        private readonly ResourceConfig _config;
        private float _current;

        public float Max => _config.Max;

        public float Current
        {
            get => _current;
            private set
            {
                if(value.Equals(_current))
                    return;
                
                _current = Mathf.Clamp(value, 0, _config.Max);
                OnChanged?.Invoke();
            }
        }

        public event Action OnChanged;

        public Resource(ResourceConfig config) => 
            _config = config;
        
        public void Refill() => 
            Current = Max;

        public virtual void AddDelta(float value) => 
            Current += value;
    }
}