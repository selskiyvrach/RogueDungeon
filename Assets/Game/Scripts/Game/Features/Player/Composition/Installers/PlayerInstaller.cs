using Game.Features.Player.App.UseCases;
using Game.Features.Player.Domain;
using Game.Features.Player.Infrastructure.Configs;
using Game.Features.Player.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Game.Features.Player.Composition.Installers
{
    // FEATURE INSTALLER
    // player.app
    // spawner
    // factory
        // INSTANCE INSTALLER
        // moveset
        // game object
        // etc
            // GAME OBJECT INSTALLER

    public class PlayerFeatureInstaller : MonoInstaller
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private PlayerConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<IFactory<Transform, Domain.Player>>().To<PlayerInstanceFactory>().AsSingle().WithArguments(new object[]{_config});
            Container.Bind<PlayerSpawner>().AsSingle().WithArguments(new object[]{_parent});
            Container.Bind<SpawnPlayerOnGameplayStartedUseCase>().AsSingle().NonLazy();
        }
    }
}