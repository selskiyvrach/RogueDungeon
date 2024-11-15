using System;
using System.Collections.Generic;
using System.Linq;

namespace RogueDungeon.Gameplay
{
    public class GameplayEntityFactory 
    {
        
    }

    public interface IGameplayEvent
    {
        
    }
    
    public class EntitiesRegistry : IGameplayEntitiesRegistry
    {
        private readonly Dictionary<Type, HashSet<IGameplayEntity>> _entities = new();
        
        public void Register<T>(T entity) where T : IGameplayEntity => 
            GetEntitiesOfType<T>().Add(entity);

        public void Unregister<T>(T entity) where T : IGameplayEntity => 
            GetEntitiesOfType<T>().Remove(entity);

        public IEnumerable<T> GetEntities<T>(Predicate<T> predicate = null) where T : IGameplayEntity
        {
            var typedSet = GetEntitiesOfType<T>().Cast<T>();
            return predicate == null 
                ? typedSet 
                : typedSet.Where(predicate.Invoke);
        }

        private HashSet<IGameplayEntity> GetEntitiesOfType<T>() where T : IGameplayEntity
        {
            var key = typeof(T);
            if (!_entities.ContainsKey(key)) 
                _entities.Add(key, new HashSet<IGameplayEntity>());
            return _entities[key];
        }
    }

    public interface IGameplayEntitiesRegistry
    {
        void Register<T>(T entity) where T : IGameplayEntity;
        void Unregister<T>(T entity) where T : IGameplayEntity;
        IEnumerable<T> GetEntities<T>(Predicate<T> predicate = null) where T : IGameplayEntity;
    }

    public interface IGameplayEntity
    {
        public void Enable();
        public void Disable();
        public void RegisterSelf(IGameplayEntitiesRegistry registry);
        public void UnregisterSelf(IGameplayEntitiesRegistry registry);
    }

    public readonly struct GameplayEntityCreatedEvent<T> : IGameplayEvent
    {
        public readonly T Entity;

        public GameplayEntityCreatedEvent(T entity) => 
            Entity = entity;
    }
}