using System.Collections.Generic;
using Zenject;

namespace Common.UtilsZenject
{
    public class DependenciesAutoResolver : MonoInstaller
    {
        private abstract class DependencyAutoResolver
        {
            public abstract void Resolve();
        }
        private class DependencyAutoResolver<T> : DependencyAutoResolver
        {
            private readonly DiContainer _container;

            public DependencyAutoResolver(DiContainer container) => 
                _container = container;

            public override void Resolve() => 
                _container.Instantiate<T>();
        }
        
        private readonly List<DependencyAutoResolver> _resolvers = new();

        public override void Start()
        {
            base.Start();
            _resolvers.ForEach(n => n.Resolve());
        }

        public override void InstallBindings() => 
            Container.InstanceSingle(this);

        public void Add<T>(DiContainer container) => 
            _resolvers.Add(new DependencyAutoResolver<T>(container));
    }
}