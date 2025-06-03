using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.Hands;
using Libs.Fsm;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Factories
{
    public class PlayerFactory : IFactory<Transform, Domain.Player>
    {
        private readonly PlayerConfig _config;
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container, PlayerConfig config)
        {
            _container = container;
            _config = config;
        }

        public Domain.Player Create(Transform parent)
        {
            var player = _container.Instantiate<Domain.Player>(new object[] { _config });
            player.SetBehaviours(CreateHandsBehaviour(), CreateCommonBehaviour());
            return player;
        }

        private PlayerHandsBehaviour CreateHandsBehaviour()
        {
            throw new System.NotImplementedException();
        }

        private StateMachine CreateCommonBehaviour()
        {
            throw new System.NotImplementedException();
        }
    }
}