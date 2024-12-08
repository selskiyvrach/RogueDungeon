using Common.GameObjectMarkers;
using Common.Prameters;
using Common.Registries;
using Common.ZenjectUtils;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Entities;
using RogueDungeon.Parameters;
using RogueDungeon.Player.Behaviours;
using RogueDungeon.PlayerInput;
using RogueDungeon.Weapons;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Installers
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
            var timings = new Parameters<Timings>();
            _playerConfig.TimingsConfig.ApplyToParameters(timings);
            container.InstanceSingle(timings);

            var charParameters = new Parameters<CharacterParameter>();
            _playerConfig.CharacterParametersConfig.ApplyToParameters(charParameters);
            container.InstanceSingle(charParameters);
            
            container.NewSingleInterfaces<PlayerBehavioursMediator>();
            container.NewSingle<ICharacterInput, CharacterInput>();
            
            container.NewSingle<MovementBehaviour>();
            container.NewSingle<WeaponBehaviour>();

            container.NewSingleResolve<Player>();
        }
    }
}