using System.Collections.Generic;
using Common.UtilsDotNet;
using Zenject;

namespace Common.Fsm
{
    public class TypeBasedStatesProviderWithCache : ITypeBasedStatesProvider
    {
        private readonly DiContainer _container;
        private readonly List<IState> _cache = new();

        public TypeBasedStatesProviderWithCache(DiContainer container) => 
            _container = container;

        public T Get<T>() where T : class, IState => 
            _cache.Get<T>() ?? _cache.With(_container.Instantiate<T>()).Get<T>();
    }
}