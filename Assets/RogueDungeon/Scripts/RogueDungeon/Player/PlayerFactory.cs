using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerFactory : IFactory<PlayerConfig, Transform, Player>
    {
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container) => 
            _container = container;

        public Player Create(PlayerConfig config, Transform parent)
        {
            var player = _container
                .InstantiatePrefab(config.Prefab, parent)
                .GetComponent<Context>().Container.Resolve<Player>();
            _container.InstanceSingle(player);
            return player;
        }
    }
}