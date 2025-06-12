using Game.Features.Levels.Domain;
using Game.Features.Levels.Infrastructure.Configs;
using Game.Features.Levels.Infrastructure.Factories;
using UnityEngine;
using Zenject;
using LevelFactory = Game.Features.Levels.Infrastructure.Factories.LevelFactory;

namespace Game.Features.Levels.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Transform _roomsParent;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<RoomFactory>().AsSingle().WithArguments(new object[] {_roomsParent});
            Container.BindInterfacesAndSelfTo<LevelFactory>().AsSingle();
            Container.Bind<Level>().FromMethod(() => Container.Resolve<LevelFactory>().Create(_levelConfig)).AsSingle();
        }
    }
}