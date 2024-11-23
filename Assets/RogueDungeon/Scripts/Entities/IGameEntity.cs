using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Services.Registries;

namespace RogueDungeon.Entities
{
    public interface IGameEntity
    {
        IRegistry<Parameter> Parameters { get; }
        IRegistry<Property> Properties { get; }
    }

    public interface IRootEntity : IGameEntity
    {
    }

    public interface INestedEntity : IGameEntity
    {
        IGameEntity Parent { get; }
    }
}