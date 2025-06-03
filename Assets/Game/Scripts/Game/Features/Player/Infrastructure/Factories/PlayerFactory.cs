using Game.Features.Player.Infrastructure.Configs;
using Libs.Fsm;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Factories
{
    public class PlayerInstanceFactory : IFactory<Transform, Domain.Player>
    {
        private readonly PlayerConfig _config;
        private readonly DiContainer _container;

        public PlayerInstanceFactory(DiContainer container, PlayerConfig config)
        {
            _container = container;
            _config = config;
        }

        public Domain.Player Create(Transform parent)
        {
            _container.BindInterfacesAndSelfTo<PlayerConfig>().FromInstance(_config).AsSingle();
            var container = _container.InstantiatePrefab(_config.Prefab, parent).GetComponent<Context>().Container; 
            var player = container.Resolve<Domain.Player>();
            player.SetBehaviours(/*container.Resolve<PlayerHandsBehaviour>(),*/ container.Resolve<StateMachine>());
            return player;
        }
    }
}