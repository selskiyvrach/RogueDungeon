using Game.Features.Levels.Domain;
using Game.Features.Levels.Infrastructure.Configs;
using Game.Features.Levels.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Game.Features.Levels.Infrastructure.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private Transform _roomsParent;
        [SerializeField] private RoomSpritesConfig _roomSpritesConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RoomSpritesConfig>().FromInstance(_roomSpritesConfig).AsSingle();
            Container.BindInterfacesTo<RoomFactory>().AsSingle().WithArguments(new object[] {_roomsParent});
            Container.BindInterfacesTo<LevelFactory>().AsSingle();
            Container.Bind<Level>().FromMethod(() => Container.Resolve<IFactory<ILevelConfig, Level>>().Create(_levelConfig)).AsSingle();
        }
    }
}