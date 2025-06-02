using Game.Features.Player.Domain;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Factory
{
    public class PlayerFactory : IFactory<PlayerConfig, Transform, Domain.Player>
    {
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container) => 
            _container = container;

        public Domain.Player Create(PlayerConfig config, Transform parent)
        {
            // should create all the systems and handle two-step initialization for some of them, like hands
            
            // player hud factory is a different factory called by gameplay to create hud and resolve mvp dependencies  
            
            
            var player = _container.InstantiatePrefab(config.Prefab, parent).GetComponent<Context>().Container.Resolve<Domain.Player>();
            _container.InstanceSingle(player);
            return player;
        }
    }
}