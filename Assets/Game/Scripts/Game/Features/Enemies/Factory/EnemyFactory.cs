using Game.Features.Enemies.Domain;
using Libs.Utils.Zenject;
using Zenject;

namespace Game.Features.Enemies.Factory
{
    public class EnemyFactory : IFactory<EnemyFactoryArgs, Enemy>
    {
        private readonly DiContainer _container;

        public EnemyFactory(DiContainer container) => 
            _container = container;

        public Enemy Create(EnemyFactoryArgs param1)
        {
            var config = param1.Config;
            
            var container = _container.CreateSubContainer();
            container.InstanceSingle(config);
            var enemy = container.InstantiatePrefab(config.Prefab, param1.Parent);
            return enemy.GetComponent<Context>().Container.Resolve<Enemy>();
        }
    }
}