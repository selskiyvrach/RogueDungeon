using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace Player.Model
{
    public class PlayerFactory : IFactory<PlayerConfig, Transform, PlayerModel>
    {
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container) => 
            _container = container;

        public PlayerModel Create(PlayerConfig config, Transform parent)
        {
            var player = _container
                .InstantiatePrefab(config.Prefab, parent)
                .GetComponent<Context>().Container.Resolve<PlayerModel>();
            _container.InstanceSingle(player);
            return player;
        }
    }
}