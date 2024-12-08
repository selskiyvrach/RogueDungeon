using Common.DotNetUtils;
using Common.GameObjectMarkers;
using Common.Registries;
using Common.ZenjectUtils;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using RogueDungeon.Player.Installers;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerParentObject _playerRootObject;
        [SerializeField] private PlayerFactory _playerFactory;
        
        public override void InstallBindings()
        {
            Container.InstanceSingle(_playerRootObject.ThrowIfNull());
            Container.NewSingle<IRegistry<IGameEntity>, Registry<IGameEntity>>();
            Container.NewSingle<ICollisionsDetector, CollisionsDetector>();
            Container.Bind<IFactory<Player.Player>>().To<PlayerFactory>().FromNewScriptableObject(_playerFactory.ThrowIfNull()).AsSingle();
            var factory = Container.Resolve<IFactory<Player.Player>>();
            var player = factory.Create();
        }
    }
}