using RogueDungeon.Collisions;

namespace RogueDungeon.Entities
{
    public interface IGameEntity
    {
        Positions Position { get; }
        void Enable();
        void Disable();
    }
}