using Common.UtilsZenject;
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
            var enemy = _container.InstantiatePrefab(config.Prefab, args.Parent);
            var enemyContainer = enemy.GetComponent<Context>().Container;
            enemyContainer.InstanceSingle(config);
            enemyContainer.InstanceSingle(enemy.transform);
            return enemyContainer.Resolve<Enemy>();
        }
    }
}