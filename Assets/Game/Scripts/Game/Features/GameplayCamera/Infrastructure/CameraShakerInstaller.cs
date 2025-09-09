using Game.Features.GameplayCamera.App;
using Game.Features.GameplayCamera.Domain;
using UnityEngine;
using Zenject;

namespace Game.Features.GameplayCamera.Infrastructure
{
    public class CameraShakerInstaller : MonoInstaller
    {
        [SerializeField] private CameraShakerConfig _config;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CameraShaker>().AsSingle().WithArguments(_config);
            Container.BindInterfacesTo<ShakeCameraOnCombatEventUseCase>().AsSingle().NonLazy();
        }
    }
}