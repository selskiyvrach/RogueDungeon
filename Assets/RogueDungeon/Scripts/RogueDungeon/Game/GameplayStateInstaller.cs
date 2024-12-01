using Common.InstallerGenerator;
using Common.Registries;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/GameplayInstaller", fileName = "GameplayInstaller", order = 0)]
    public class GameplayStateInstaller : ScriptableInstaller
    {
        public override void Install(DiContainer container)
        {
            base.Install(container);
            container.Bind<IRegistry<IGameEntity>>().To<Registry<IGameEntity>>().FromNew().AsSingle();
            container.Bind<ICollisionsDetector>().To<CollisionsDetector>().FromNew().AsSingle();
        }
    }
}