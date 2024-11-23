using System;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.Registries;

namespace RogueDungeon.Entities
{
    public abstract class Entity : IGameEntity, IDisposable
    {
        private readonly Registry<Property> _properties = new();
        private readonly Registry<Parameter> _parameters = new();
        
        protected readonly IEventBus<IRootEvent> GameEvents;
        protected readonly IRegistry<IRootEntity> GameEntities;
        public IRegistry<Property> Properties => _properties;
        public IRegistry<Parameter> Parameters => _parameters;
        
        protected Entity(IEventBus<IRootEvent> gameEvents, IRegistry<IRootEntity> gameEntities)
        {
            GameEvents = gameEvents;
            GameEntities = gameEntities;
        }

        public void Dispose() => 
            Properties.Dispose();
    }

    public class RootEntity : Entity, IRootEntity
    {
        public RootEntity(IEventBus<IRootEvent> gameEvents, IRegistry<IRootEntity> gameEntities) : base(gameEvents, gameEntities)
        {
        }
    }
}