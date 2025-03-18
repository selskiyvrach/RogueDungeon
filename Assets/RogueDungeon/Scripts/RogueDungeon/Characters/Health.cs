using System;

namespace Characters
{
    public class Health : IResource
    {
        private float _max;
        private float _current;

        public float Current
        {
            get => _current;
            set
            {
                _current = value;
                OnChanged?.Invoke();
            }
        }

        public float Max
        {
            get => _max;
            set
            {
                _max = value;
                OnChanged?.Invoke();
            }
        }
        
        public bool IsAlive => Current > 0;

        public event Action OnChanged;
    }
}