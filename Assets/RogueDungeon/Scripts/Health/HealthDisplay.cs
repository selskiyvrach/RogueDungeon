using UnityEngine;

namespace RogueDungeon.Health
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;
        
        private Health _health;

        public void Construct(Health health)
        {
            _health = health;
            _health.OnChanged += HandleHealthChanged;
        }

        private void HandleHealthChanged(HealthChangeReason _)
        {
            var redAmount = 1 - _health.Current / _health.Max;
            SetAmountDisplay(redAmount);
        }

        private void SetAmountDisplay(float redAmount)
        {
            foreach (var renderer in _meshRenderers)
                renderer.material.color = Color.Lerp(Color.white, Color.red, redAmount);
        }
    }
}