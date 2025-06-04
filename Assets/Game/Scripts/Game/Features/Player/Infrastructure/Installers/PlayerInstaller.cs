using Game.Features.Player.App.UseCases;
using Game.Features.Player.Domain;
using Game.Features.Player.Domain.Behaviours.CommonMoveset;
using Game.Features.Player.Infrastructure.Configs;
using Game.Features.Player.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Infrastructure.Installers
{
    public class PlayerFeatureInstaller : MonoInstaller
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private PlayerConfig _config;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerConfig>().FromInstance(_config).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelTraverserContext>().FromNew().AsSingle();
            Container.Bind<IFactory<Transform, Domain.Player>>().To<PlayerInstanceFactory>().AsSingle();
            Container.Bind<PlayerSpawner>().AsSingle().WithArguments(new object[]{_parent});
            
            Container.Bind<SpawnPlayerOnGameplayStartedUseCase>().AsSingle().NonLazy();
            Container.Bind<SyncLevelAndLevelContextUseCase>().AsSingle().NonLazy();
        }
    }
}