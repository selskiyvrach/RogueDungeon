using System.Collections.Generic;

namespace Game.Features.Enemies.Domain
{
    public class EnemyStatesProvider
    {
        private readonly IFactory<string, EnemyMove> _statesFactory;
        private readonly Dictionary<string, EnemyMove> _cache = new();

        public EnemyStatesProvider(IFactory<string, EnemyMove> statesFactory) => 
            _statesFactory = statesFactory;

        public EnemyMove GetState(string name)
        {
            if(!_cache.ContainsKey(name))
                _cache.Add(name, _statesFactory.Create(name));
            return _cache[name];
        }
    }
}