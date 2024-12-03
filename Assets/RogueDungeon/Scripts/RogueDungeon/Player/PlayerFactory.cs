using Common.GameObjectMarkers;
using Common.Prameters;
using Common.Properties;
using Common.Registries;
using Common.ZenjectUtils;
using RogueDungeon.Behaviours;
using RogueDungeon.Behaviours.AttackBehaviour;
using RogueDungeon.Behaviours.DodgeBehaviour;
using RogueDungeon.Entities;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInputCommands;
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
            var timings = new Parameters<Timings>();
            _playerConfig.TimingsConfig.ApplyToParameters(timings);
            container.InstanceSingle(timings);

            var charParameters = new Parameters<CharacterParameter>();
            _playerConfig.CharacterParametersConfig.ApplyToParameters(charParameters);
            container.InstanceSingle(charParameters);

            container.InstanceSingle<IRegistry<Property>, Registry<Property>>(new Registry<Property>
            {
                container.InstanceSingleInterfacesAndSelf(new Property<DodgeState>()),
                container.InstanceSingleInterfacesAndSelf(new Property<AttackState>()),
            });
            
            container.NewSingle<CharacterControlStateResolver>();
            container.NewSingle<ICharacterInput, CharacterInput>();
            
            container.NewSingle<DodgeBehaviour>();
            container.NewSingle<AttackBehaviour>();

            container.Resolve<DodgeBehaviour>();
            container.NewSingleResolve<Player>();
        }
    }
}