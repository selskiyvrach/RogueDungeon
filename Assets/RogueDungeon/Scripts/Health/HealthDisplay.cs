using UnityEngine;

namespace RogueDungeon.Health
{
    public abstract class HealthDisplay : MonoBehaviour
    {
        public abstract void HandleHealthChanged(Health health, HealthChangeReason _);
    }
}