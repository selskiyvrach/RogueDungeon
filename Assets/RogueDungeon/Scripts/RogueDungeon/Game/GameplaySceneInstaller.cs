using Common.GameObjectMarkers;
using Common.InstallerGenerator;
using Common.Registries;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using Zenject;

namespace RogueDungeon.Game
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRegistry<IGameEntity>>().To<Registry<IGameEntity>>().FromNew().AsSingle();
            Container.Bind<ICollisionsDetector>().To<CollisionsDetector>().FromNew().AsSingle();
            Container.NewSingle<ISpawner<Player.Player>, Spawner<Player.Player, PlayerParentObject>>();
            Container.Bind<GameplayController>().AsSingle().NonLazy();
        }
    }
}