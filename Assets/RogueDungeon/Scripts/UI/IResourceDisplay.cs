using RogueDungeon.CharacterResource;

namespace RogueDungeon.UI
{
    public interface IResourceDisplay
    {
        void HandleChanged(Resource resource, ResourceChangeReason _);
        void Tick();
    }
}