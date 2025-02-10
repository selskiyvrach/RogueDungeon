using System;
using System.Collections.Generic;
using Common.UtilsDotNet;
using Zenject;

namespace Common.Fsm
{
    public class StatesProviderWithCache : ITypeBasedStatesProvider
    {
        private readonly DiContainer _container;
        private readonly List<IState> _cache = new();

        public StatesProviderWithCache(DiContainer container) => 
            _container = container;

        public T Get<T>() where T : class, IState => 
            _cache.Get<T>() ?? _cache.With(_container.Instantiate<T>()).Get<T>();

        protected void Create(Type type)
        {
            if(_cache.Get(n => n.GetType() == type) == null)
                _cache.Add((IState)_container.Instantiate(type));
        }
    }
}