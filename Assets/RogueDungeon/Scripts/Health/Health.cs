using UnityEngine;

namespace RogueDungeon.Health
{
    public class Health
    {
        private float _current;

        public float Current { get; private set; }

        public float Max { get; private set; }

        public event System.Action<HealthChangeReason> OnChanged;

        public void TakeDamage(float damage) => 
            SetHealth(Max, Current - damage, HealthChangeReason.Damage);

        public void SetHealth(float max, float current, HealthChangeReason reason)
        {
            Max = max;
            Current = Mathf.Clamp(_current + current, 0, Max);
            OnChanged?.Invoke(reason);
        }
    }

    public enum HealthChangeReason
    {
        Damage = 10,
        Heal = 50,
        Recalculated = 100,
    }
}