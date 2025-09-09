using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class EnemyFactory : IFactory<string, Transform, Enemy>
    {
        private readonly DiContainer _container;
        private readonly IEnemyConfigsRepository _enemyConfigsRepository;

        public EnemyFactory(DiContainer container, IEnemyConfigsRepository enemyConfigsRepository)
        {
            _container = container;
            _enemyConfigsRepository = enemyConfigsRepository;
        }

        public Enemy Create(string id, Transform parent)
        {
            var config = _enemyConfigsRepository.Get(id);
            var container = _container.CreateSubContainer();
            container.InstanceSingle(config);
            var enemy = container.InstantiatePrefab(config.Prefab, parent);
            return enemy.GetComponent<Context>().Container.Resolve<Enemy>();
        }
    }
}