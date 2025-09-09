using Game.Features.Player.Domain.Hands;
using Game.Features.Player.Infrastructure.Configs;
using Libs.Fsm;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Factories
{
    public class PlayerInstanceFactory : IFactory<Transform, Domain.Player>
    {
        private readonly DiContainer _container;

        public PlayerInstanceFactory(DiContainer container) => 
            _container = container;

        public Domain.Player Create(Transform parent)
        {
            var container = _container.InstantiatePrefab(_container.Resolve<PlayerConfig>().Prefab, parent).GetComponent<Context>().Container; 
            var player = container.Resolve<Domain.Player>();
            player.SetBehaviours(container.Resolve<PlayerHandsBehaviour>(), container.Resolve<StateMachine>());
            return player;
        }
    }
}