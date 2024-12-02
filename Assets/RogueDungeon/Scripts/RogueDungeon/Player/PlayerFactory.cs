using Common.GameObjectMarkers;
using Common.Properties;
using Common.Registries;
using Common.ZenjectUtils;
using RogueDungeon.Entities;
using RogueDungeon.PlayerInputCommands;
using Unity.Properties;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerFactory : ScriptableObject, IFactory<Player>
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerAnimationsConfig _animationsConfig;

        [Inject] private PlayerParentObject _instantiatePlayerTo;
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
            var gameObjectInstaller = Instantiate(_playerConfig.Prefab, _instantiatePlayerTo.transform);
            gameObjectInstaller.InstallToPlayerContext(container);

            container.Bind<PlayerCameraHandler>().AsSingle().NonLazy();
            container.Bind<PlayerAnimationsConfig>().FromNewScriptableObject(_animationsConfig).AsSingle();
            
            // parameter manager or smth
            container.InstanceSingle(new DodgeDuration(1));
            container.InstanceSingle(new AttackPrepareDuration(1));
            container.InstanceSingle(new AttackExecuteDuration(1));
            container.InstanceSingle(new AttackFinishDuration(1));
            
            container.NewSingle<CharacterControlStateResolver>();
            container.NewSingle<ICharacterInput, CharacterInput>();
            container.NewSingleInterfaces<Property<AttackState>>();
            container.NewSingleInterfaces<Property<DodgeState>>();
            // behaviour aggregation
            // behaviour update 
            container.NewSingle<DodgeBehaviour>();
            container.NewSingle<AttackBehaviour>();
            
            container.NewSingle<Player>();
        }
    }
}