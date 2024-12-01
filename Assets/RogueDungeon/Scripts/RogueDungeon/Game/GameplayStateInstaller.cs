using Common.DotNetUtils;
using Common.InstallerGenerator;
using Common.Registries;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/GameplayInstaller", fileName = "GameplayInstaller", order = 0)]
    public class GameplayStateInstaller : ScriptableInstaller
    {
        [SerializeField] private PlayerFactory _playerFactory;

        public override void Install(DiContainer container)
        {
            base.Install(container);
            container.Bind<IRegistry<IGameEntity>>().To<Registry<IGameEntity>>().FromNew().AsSingle();
            container.Bind<ICollisionsDetector>().To<CollisionsDetector>().FromNew().AsSingle();
            container.Bind<IFactory<Player.Player>>().To<PlayerFactory>().FromNewScriptableObject(_playerFactory.ThrowIfNull()).AsSingle();
            container.Bind<Player.Player>().FromMethod(() => container.Resolve<IFactory<Player.Player>>().Create()).AsSingle().NonLazy();
            
        }
    }
}