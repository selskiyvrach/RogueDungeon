using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyFactory : IFactory<EnemyFactoryArgs, Enemy>
    {
        private readonly DiContainer _container;

        public EnemyFactory(DiContainer container) => 
            _container = container;

        public Enemy Create(EnemyFactoryArgs args)
        {
            var config = args.Config;
            
            var container = _container.CreateSubContainer();
            container.InstanceSingle(config);
            var enemy = container.InstantiatePrefab(config.Prefab, args.Parent);
            return enemy.GetComponent<Context>().Container.Resolve<Enemy>();
        }
    }
}