using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Health
{
    public class TextureTintHealthDisplay : HealthDisplay
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;
        
        public override void HandleHealthChanged(Health health, HealthChangeReason _)
        {
            var redAmount = 1 - health.Current / health.Max;
            SetValue(redAmount);
        }

        [Button]
        private void SetValue(float redAmount)
        {
            foreach (var renderer in _meshRenderers)
                renderer.material.color = Color.Lerp(Color.white, Color.red, redAmount);
        }
    }
}