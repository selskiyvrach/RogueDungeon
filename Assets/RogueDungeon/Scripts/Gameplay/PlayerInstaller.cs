using RogueDungeon.Gameplay.InputCommands;
using RogueDungeon.Services.Events;
using RogueDungeon.Services.FSM;
using Zenject;

namespace RogueDungeon.Gameplay
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromNew().AsSingle();
            Container.Bind<CharacterAnimationRoot>().FromComponentInNewPrefabResource("Player/Player").AsSingle();
            Container.Bind<IEventBus<IAnimationEvent>>().To<EventBus<IAnimationEvent>>().FromNew().AsSingle();
            Container.Bind<PlayerAnimationsConfig>().FromScriptableObjectResource("Player/PlayerAnimationsConfig").AsSingle();
            Container.Bind<StateMachine>().FromFactory<PlayerBehaviourStateMachineFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AvailableInteractions>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<Player>().FromNew().AsSingle().NonLazy();
        }
    }
}