using Common.Registries;
using Common.UnityUtils;
using RogueDungeon.Entities;
using RogueDungeon.PlayerInputCommands;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    [CreateAssetMenu(menuName = "Installers/PlayerFactory", fileName = "PlayerFactory", order = 0)]
    public class PlayerFactory : ScriptableObject, IFactory<Player>
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerAnimationsConfig _animationsConfig;

        [Inject] private IRootObject<Player> _instantiatePlayerTo;
        [Inject] private IRegistry<IGameEntity> _gameEntitiesRegistry;
        [Inject] private DiContainer _container;
        
        public Player Create()
        {
            var playerContainer = _container.CreateSubContainer();
            InstallBindingsToPlayerContext(playerContainer);
            var player = playerContainer.Resolve<Player>();
            _gameEntitiesRegistry.Add(player);
            return player;
        }

        private void InstallBindingsToPlayerContext(DiContainer container)
        {
            var gameObjectInstaller = Instantiate(_playerConfig.Prefab, _instantiatePlayerTo.Transform);
            gameObjectInstaller.InstallToPlayerContext(container);

            container.Bind<PlayerCameraHandler>().FromNew().AsSingle();
            container.Bind<ICharacterInput>().To<CharacterInput>().FromNew().AsSingle();
            container.Bind<IRootObject<Animation>>().FromComponentInNewPrefab(_playerConfig.Prefab).AsSingle();
            container.Bind<PlayerAnimationsConfig>().FromNewScriptableObject(_animationsConfig).AsSingle();
            container.BindInterfacesAndSelfTo<Player>().FromNew().AsSingle();
        }
    }
}