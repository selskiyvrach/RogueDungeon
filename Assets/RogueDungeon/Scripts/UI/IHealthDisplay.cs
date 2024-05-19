using RogueDungeon.CharacterResource;

namespace RogueDungeon.UI
{
    public interface IHealthDisplay
    {
        void HandleHealthChanged(Resource resource, ResourceChangeReason _);
        void Tick();
    }
}