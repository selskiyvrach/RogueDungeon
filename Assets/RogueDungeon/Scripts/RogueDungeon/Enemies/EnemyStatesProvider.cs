using System.Collections.Generic;
using RogueDungeon.Enemies.States;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyStatesProvider
    {
        private readonly IFactory<EnemyStateConfig, EnemyState> _statesFactory;
        private readonly Dictionary<EnemyStateConfig, EnemyState> _cache = new();

        public EnemyStatesProvider(IFactory<EnemyStateConfig, EnemyState> statesFactory) => 
            _statesFactory = statesFactory;

        public EnemyState GetState(EnemyStateConfig config)
        {
            if(!_cache.ContainsKey(config))
                _cache.Add(config, _statesFactory.Create(config));
            return _cache[config];
        }
    }
}