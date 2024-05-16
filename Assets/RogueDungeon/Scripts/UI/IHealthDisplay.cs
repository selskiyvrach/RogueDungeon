using RogueDungeon.Health;

namespace RogueDungeon.UI
{
    public interface IHealthDisplay
    {
        void HandleHealthChanged(Health.Health health, HealthChangeReason _);
        void Tick();
    }
}