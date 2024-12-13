using System.Collections.Generic;
using Common.DotNetUtils;
using Zenject;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal class StatesFactoryWithCache : IStatesFactory
    {
        private readonly DiContainer _container;
        private readonly List<IState> _cache = new();

        public StatesFactoryWithCache(DiContainer container) => 
            _container = container;

        public T Create<T>() where T : IState => 
            _cache.Get<T>() ?? _cache.With(_container.Instantiate<T>()).Get<T>();
    }
}