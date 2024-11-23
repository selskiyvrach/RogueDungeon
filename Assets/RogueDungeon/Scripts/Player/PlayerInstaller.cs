using RogueDungeon.Animations;
using RogueDungeon.PlayerInputCommands;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using RogueDungeon.Weapons;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private PlayerAnimationsConfig _animationsConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromNew().AsSingle();
            Container.Bind<CharacterAnimationRoot>().FromComponentInNewPrefab(_playerConfig.Prefab).AsSingle();
            Container.Bind<IEventBus<IAnimationEvent>>().To<EventBus<IAnimationEvent>>().FromNew().AsSingle();
            Container.Bind<PlayerAnimationsConfig>().FromScriptableObject(_animationsConfig).AsSingle();
            Container.Bind<StateMachine>().FromFactory<PlayerBehaviourStateMachineFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponFactory>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<AvailableInteractions>().FromNew().AsSingle();
            // Container.BindInterfacesAndSelfTo<Player>().FromNew().AsSingle().NonLazy();
        }
    }
    
    public class PlayerFactory : IFactory<Entities.Player>
    {
        
        
        public Entities.Player Create()
        {
            return null;
        }
    }
}